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

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IKeyDBService _cacheDbService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        IKeyDBService cacheDbService)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _cacheDbService = cacheDbService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string loginAttemptsKey = $"loginAttempts:{request.Email}";

        var blockData = await _cacheDbService.GetValueAsync<BlockInfo>(loginAttemptsKey);

        if (blockData is not null && blockData.Attempts >= 3)
        {
            if (blockData.BlockedSince.HasValue &&
                DateTime.UtcNow - blockData.BlockedSince.Value > TimeSpan.FromHours(1))
            {
                await _cacheDbService.SetValueAsync(loginAttemptsKey, new BlockInfo
                {
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
            var newAttemptsData = new BlockInfo()
            {
                Attempts = blockData?.Attempts + 1 ?? 1,
                BlockedSince = blockData?.BlockedSince ?? DateTime.UtcNow
            };
            await _cacheDbService.SetValueAsync(loginAttemptsKey, newAttemptsData);

            Result<LoginResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.UsersLoginIncorrect);
        }

        await _cacheDbService.SetValueAsync(loginAttemptsKey, new BlockInfo
        {
            Attempts = 0,
            BlockedSince = null
        });

        (string token, var expires) = _tokenService.Generate(user);
        LoginResponse response = new(token, expires);
        return Result<LoginResponse>.Success(response);
    }
}