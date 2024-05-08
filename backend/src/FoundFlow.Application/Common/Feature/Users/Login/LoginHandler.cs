using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Extensions;
using FoundFlow.Shared.Messages;
using MediatR;
#pragma warning disable S2589

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IManagerDbService _cacheDbService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        IManagerDbService cacheDbService)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _cacheDbService = cacheDbService;
    }

    private async Task BlockCheck(BlockInfo blockData, string loginAttemptsKey, string collectionName, bool cleanData = false)
    {
        if (blockData is null)
        {
            var newAttemptsData = new BlockInfo()
            {
                EmailKey = loginAttemptsKey,
                Attempts = cleanData ? 0 : 1,
                BlockedSince = cleanData ? null : DateTime.UtcNow
            };
            await _cacheDbService.InsertValueAsync(collectionName, newAttemptsData);
        }
        else
        {
            var updateAttemptsData = new BlockInfo()
            {
                Id = blockData.Id,
                EmailKey = loginAttemptsKey,
                Attempts = cleanData ? 0 : blockData.Attempts + 1,
                BlockedSince = cleanData ? null : blockData.BlockedSince ?? DateTime.UtcNow
            };
            await _cacheDbService.UpdateValueAsync(collectionName, "EmailKey", loginAttemptsKey, updateAttemptsData);
        }
    }

    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string loginAttemptsKey = $"loginAttempts:{request.Email}";
        string collectionName = "BlockInfo";

        var blockData = await _cacheDbService.GetValueAsync<BlockInfo>(collectionName, "EmailKey", loginAttemptsKey);

        if (blockData?.Attempts >= 3)
        {
            if (blockData.BlockedSince.HasValue &&
                DateTime.UtcNow - blockData.BlockedSince.Value > TimeSpan.FromHours(1))
            {
                await _cacheDbService.UpdateValueAsync(collectionName, "EmailKey", loginAttemptsKey, new BlockInfo
                {
                    EmailKey = loginAttemptsKey,
                    Attempts = 0,
                    BlockedSince = null
                });
            }
            else
            {
                Result<LoginResponse>.Failure(HttpStatusCode.Forbidden, ErrorMessages.UserLoginAccountTemporaryBlocked);
            }
        }

        var user = await _unitOfWork.UsersRepository
            .FindOneAsync(
                user =>
                    user.Email == request.Email.ToLower(CultureInfo.CurrentCulture),
                cancellationToken);

        if (user == null)
            Result<LoginResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UsersLoginIncorrect);

        if (user!.Blocked.HasValue && user.Blocked.Value)
            Result<LoginResponse>.Failure(HttpStatusCode.Forbidden, ErrorMessages.UsersLoginAccountIsBlocked);

        bool isPasswordValid = Crypto.Verify(request.Password, user!.Password);

        if (!isPasswordValid)
        {
            await BlockCheck(blockData, loginAttemptsKey, collectionName);

            Result<LoginResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.UsersLoginIncorrect);
        }

        await BlockCheck(blockData, loginAttemptsKey, collectionName, true);

        (string token, var expires) = _tokenService.Generate(user);
        LoginResponse response = new(token, expires);
        return Result<LoginResponse>.Success(response);
    }

}