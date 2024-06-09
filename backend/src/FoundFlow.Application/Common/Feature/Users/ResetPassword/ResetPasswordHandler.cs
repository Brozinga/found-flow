using FoundFlow.Application.Common.Feature.Users.Login;
using FoundFlow.Application.Common.Feature.Users.Update;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Extensions;
using FoundFlow.Shared.Messages;
using MediatR;
using SharpCompress.Common;
using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;
public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, Result<ResetPasswordResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordHandler(
    IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private Domain.Entities.Users ConvertToAgreggate(Domain.Entities.Users userData, string hashPassword)
    {
        var user = new Domain.Entities.Users(
            userData.Id,
            userData.UserName,
            userData.Email,
            hashPassword,
            userData.NotificationEnabled,
            userData.Blocked,
            DateTime.SpecifyKind(userData.CreationDate, DateTimeKind.Utc));

        return user;
    }

    public async Task<Result<ResetPasswordResponse>> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository
           .FindOneAsync(
               user =>
                   user.Email == request.Email.ToLower(CultureInfo.CurrentCulture),
               cancellationToken);

        if (user == null)
            Result<LoginResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        string newPassword = PasswordExtensions.GenerateRandomPassword();

        string hashedPassword = Crypto.Hash(newPassword);

        var entity = ConvertToAgreggate(user, hashedPassword);

        _ = _unitOfWork.UsersRepository.Update(entity);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<ResetPasswordResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<ResetPasswordResponse>.Success(HttpStatusCode.OK, new ResetPasswordResponse(newPassword));
    }
}
