using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Common.Feature.Categories.Update;
using FoundFlow.Application.Common.Feature.Transactions.Create;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Transactions.Update;

public class UpdateTransactionHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTransactionRequest, Result<UpdateTransactionResponse>>
{
    public async Task<Result<UpdateTransactionResponse>> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<UpdateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var category = await unitOfWork.CategoriesRepository.FindOneAsync(category => category.Id == request.CategorieId, cancellationToken);

        if (category is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.CategoriesCategorieNotFoundMessage);

        var transaction = await unitOfWork.TransactionsRepository.FindOneAsync(transaction => transaction.Id == request.Id, cancellationToken);

        if (transaction is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.TransactionsTransactionNotFoundMessage);

        var entity = ConvertToAggregate(request, transaction, category, user);
        _ = unitOfWork.TransactionsRepository.Update(entity);
        int isSaved = await unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<UpdateTransactionResponse>.Success(HttpStatusCode.NoContent, new UpdateTransactionResponse(entity.Id));
    }

    private static Domain.Entities.Transactions ConvertToAggregate(UpdateTransactionRequest request, Domain.Entities.Transactions transactionData, Domain.Entities.Categories categorie, Domain.Entities.Users user)
    {
        return new(
            request.Id,
            categorie,
            user,
            new TransactionValueObject(
                request.Title,
                request.Amount,
                request.TransactionType,
                request.PaymentStatus),
            DateTime.SpecifyKind(transactionData.CreationDate, DateTimeKind.Utc),
            DateTime.SpecifyKind(request.PaymentDate, DateTimeKind.Utc));
    }
}