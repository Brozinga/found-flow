using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Transactions.Delete;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionRequest, Result<DeleteTransactionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

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