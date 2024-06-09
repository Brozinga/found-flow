using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Transactions.Create;

public class CreateTransactionsHandler : IRequestHandler<CreateTransactionRequest, Result<CreateTransactionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionsHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<Result<CreateTransactionResponse>> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var categorie = await _unitOfWork.CategoriesRepository.FindOneAsync(categorie => categorie.Id == request.CategorieId, cancellationToken);

        if (categorie is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.CategoriesCategorieNotFoundMessage);

        var entity = ConvertToAgreggate(request, user, categorie);

        _ = await _unitOfWork.TransactionsRepository.AddAsync(entity, cancellationToken);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<CreateTransactionResponse>.Success(HttpStatusCode.Created, new CreateTransactionResponse(entity.Id));
    }

    private Domain.Entities.Transactions ConvertToAgreggate(CreateTransactionRequest request, Domain.Entities.Users user, Domain.Entities.Categories categorie)
    {
        return new(
            categorie,
            user,
            request.Title,
            request.Amount,
            request.TransactionType,
            request.PaymentStatus,
            DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            DateTime.SpecifyKind(request.PaymentDate, DateTimeKind.Utc));
    }

}