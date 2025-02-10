using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Transactions.Delete;

/// <summary>
/// Manipulador (Handler) para a solicitação de exclusão de uma transação (`DeleteTransactionRequest`).
/// </summary>
/// <remarks>
/// Cria uma nova instância de `DeleteTransactionHandler`.
/// </remarks>
/// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
public class DeleteTransactionHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTransactionRequest, Result<DeleteTransactionResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Manipula a solicitação de exclusão de uma transação.
    /// </summary>
    /// <param name="request">A solicitação contendo o ID da transação a ser excluída.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `DeleteTransactionResponse` se a transação for excluída com sucesso,
    /// ou uma mensagem de erro em caso de falha (por exemplo, transação não encontrada, usuário não encontrado ou erro no banco de dados).
    /// </returns>
    public async Task<Result<DeleteTransactionResponse>> Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<DeleteTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var transaction = await _unitOfWork.TransactionsRepository
            .FindOneAsync(
                transaction =>
                    transaction.Id == request.Id,
                cancellationToken);

        if (transaction is null)
            Result<DeleteTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.TransactionsTransactionNotFoundMessage);

        var entity = _unitOfWork.TransactionsRepository.Delete(transaction);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<DeleteTransactionResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<DeleteTransactionResponse>.Success(HttpStatusCode.OK, new DeleteTransactionResponse(entity.Id));
    }
}