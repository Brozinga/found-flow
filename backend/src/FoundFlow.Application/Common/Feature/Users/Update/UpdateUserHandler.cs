using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Extensions;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Update;

/// <summary>
/// Manipulador (Handler) para a solicitação de atualização de um usuário (`UpdateUserRequest`).
/// </summary>
public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, Result<UpdateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Cria uma nova instância de `UpdateUserHandler`.
    /// </summary>
    /// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
    public UpdateUserHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <summary>
    /// Converte uma solicitação `UpdateUserRequest` e os dados existentes do usuário em uma entidade `Users` atualizada, incluindo a nova senha criptografada.
    /// </summary>
    /// <param name="request">A solicitação contendo os novos dados do usuário.</param>
    /// <param name="userData">Os dados atuais do usuário no banco de dados.</param>
    /// <param name="hashPassword">A nova senha do usuário criptografada (hashed).</param>
    /// <returns>A entidade `Users` atualizada com os novos dados e a nova senha.</returns>
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

    /// <summary>
    /// Converte uma solicitação `UpdateUserRequest` e os dados existentes do usuário em uma entidade `Users` atualizada, sem atualizar a senha.
    /// </summary>
    /// <param name="request">A solicitação contendo os novos dados do usuário.</param>
    /// <param name="userData">Os dados atuais do usuário no banco de dados.</param>
    /// <returns>A entidade `Users` atualizada com os novos dados, mantendo a senha antiga.</returns>
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

    /// <summary>
    /// Manipula a solicitação de atualização de um usuário, atualizando seus dados no banco de dados.
    /// </summary>
    /// <param name="request">A solicitação contendo os novos dados do usuário.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `UpdateUserResponse` se o usuário for atualizado com sucesso,
    /// ou uma mensagem de erro em caso de falha (por exemplo, usuário não encontrado ou erro no banco de dados).
    /// </returns>
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