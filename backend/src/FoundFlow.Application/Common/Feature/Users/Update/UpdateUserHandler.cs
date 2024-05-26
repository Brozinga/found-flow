using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Extensions;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Update;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, Result<UpdateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    private Domain.Entities.Users ConvertToAgreggate(UpdateUserRequest request, Domain.Entities.Users userData, string hashPassword)
    {
        var user = new Domain.Entities.Users(
            request.UserId,
            request.UserName,
            userData.Email,
            hashPassword,
            request.Notification,
            userData.Blocked,
            DateTime.SpecifyKind(userData.CreationDate, DateTimeKind.Utc));

        return user;
    }

    private Domain.Entities.Users ConvertToAgreggateNoUpdatePassword(UpdateUserRequest request, Domain.Entities.Users userData)
    {
        var user = new Domain.Entities.Users(
            request.UserId,
            request.UserName,
            userData.Email,
            userData.Password,
            request.Notification,
            userData.Blocked,
            DateTime.SpecifyKind(userData.CreationDate, DateTimeKind.Utc));

        return user;
    }

    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository
            .FindOneAsync(
                user =>
                    user.Id == request.UserId,
                cancellationToken);

        if (user is null)
            Result<UpdateUserResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UsersNotFoundMessage);

        Domain.Entities.Users entity = null;

        if (string.IsNullOrEmpty(request.Password))
        {
            entity = ConvertToAgreggateNoUpdatePassword(request, user);
        }
        else
        {
            string hashedPassword = Crypto.Hash(request.Password);
            entity = ConvertToAgreggate(request, user, hashedPassword);
        }

        _ = _unitOfWork.UsersRepository.Update(entity);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<UpdateUserResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<UpdateUserResponse>.Success(HttpStatusCode.NoContent, new UpdateUserResponse(entity.Id));
    }

}