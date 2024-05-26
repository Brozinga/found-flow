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

namespace FoundFlow.Application.Common.Feature.Users.Create;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    private Domain.Entities.Users ConvertToAgreggate(CreateUserRequest request, string hashPassword)
    {
        var user = new Domain.Entities.Users(
            request.UserName,
            request.Email,
            hashPassword,
            request.Notification,
            false,
            DateTime.UtcNow);

        return user;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository
            .FindOneAsync(
                user =>
                    user.Email == request.Email.ToLower(CultureInfo.CurrentCulture),
                cancellationToken);

        if (user is not null)
            Result<CreateUserResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.UsersEmailIsRegisteredMessage);

        string hashedPassword = Crypto.Hash(request.Password);

        var entity = ConvertToAgreggate(request, hashedPassword);

        _ = await _unitOfWork.UsersRepository.AddAsync(entity, cancellationToken);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<CreateUserResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<CreateUserResponse>.Success(HttpStatusCode.Created, new (entity.Id));
    }

}