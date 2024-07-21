using FoundFlow.Application.Common.Feature.Users.Login;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Extensions;
using FoundFlow.Shared.Messages;
using MediatR;
using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;

/// <summary>
/// Manipulador (Handler) para a solicitação de redefinição de senha do usuário (`ResetPasswordRequest`).
/// </summary>
public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, Result<ResetPasswordResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Cria uma nova instância de `ResetPasswordHandler`.
    /// </summary>
    /// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
    public ResetPasswordHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <summary>
    /// Converte um usuário existente (`userData`) e uma nova senha criptografada (`hashPassword`) em uma entidade `Users` atualizada.
    /// </summary>
    /// <param name="userData">Os dados do usuário a ser atualizado.</param>
    /// <param name="hashPassword">A nova senha do usuário criptografada (hashed).</param>
    /// <returns>A entidade `Users` atualizada com a nova senha.</returns>
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

    /// <summary>
    /// Manipula a solicitação de redefinição de senha, gerando uma nova senha, criptografando-a, atualizando o usuário no banco de dados e retornando a nova senha.
    /// </summary>
    /// <param name="request">A solicitação contendo o e-mail do usuário que deseja redefinir a senha.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `ResetPasswordResponse` com a nova senha gerada,
    /// ou uma mensagem de erro em caso de falha (por exemplo, usuário não encontrado ou erro no banco de dados).
    /// </returns>
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
