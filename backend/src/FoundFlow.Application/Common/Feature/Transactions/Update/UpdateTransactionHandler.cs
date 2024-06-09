using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Common.Feature.Categories.Update;
using FoundFlow.Application.Common.Feature.Transactions.Create;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Transactions.Update;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionRequest, Result<UpdateTransactionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransactionHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<Result<UpdateTransactionResponse>> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<UpdateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var categorie = await _unitOfWork.CategoriesRepository.FindOneAsync(categorie => categorie.Id == request.CategorieId, cancellationToken);

        if (categorie is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.CategoriesCategorieNotFoundMessage);

        var transaction = await _unitOfWork.TransactionsRepository.FindOneAsync(transaction => transaction.Id == request.Id, cancellationToken);

        if (transaction is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.TransactionsTransactionNotFoundMessage);

        var entity = ConvertToAgreggate(request, transaction, categorie, user);
        _ = _unitOfWork.TransactionsRepository.Update(entity);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<UpdateTransactionResponse>.Success(HttpStatusCode.NoContent, new UpdateTransactionResponse(entity.Id));
    }

    private Domain.Entities.Transactions ConvertToAgreggate(UpdateTransactionRequest request, Domain.Entities.Transactions transactionData, Domain.Entities.Categories categorie, Domain.Entities.Users user)
    {
        return new(
            request.Id,
            categorie,
            user,
            request.Title,
            request.Amount,
            request.TransactionType,
            request.PaymentStatus,
            DateTime.SpecifyKind(transactionData.CreationDate, DateTimeKind.Utc),
            DateTime.SpecifyKind(request.PaymentDate, DateTimeKind.Utc));
    }
}