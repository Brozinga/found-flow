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

/// <summary>
/// Manipulador (Handler) para a solicitação de criação de um novo usuário (`CreateUserRequest`).
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Cria uma nova instância de `CreateUserHandler`.
    /// </summary>
    /// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
    public CreateUserHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <summary>
    /// Converte uma solicitação `CreateUserRequest` em uma entidade `Users`.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados do novo usuário.</param>
    /// <param name="hashPassword">A senha do usuário criptografada (hashed).</param>
    /// <returns>A entidade `Users` convertida.</returns>
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

    /// <summary>
    /// Manipula a solicitação de criação de um novo usuário.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados do novo usuário.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `CreateUserResponse` se o usuário for criado com sucesso,
    /// ou uma mensagem de erro em caso de falha (por exemplo, e-mail já cadastrado ou erro no banco de dados).
    /// </returns>
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