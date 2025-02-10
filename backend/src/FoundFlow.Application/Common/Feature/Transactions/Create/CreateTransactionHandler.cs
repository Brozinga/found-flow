using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Entities;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Transactions.Create;

/// <summary>
/// Manipulador (Handler) para a solicitação de criação de uma nova transação (`CreateTransactionRequest`).
/// </summary>
/// <remarks>
/// Cria uma nova instância de `CreateTransactionsHandler`.
/// </remarks>
/// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
public class CreateTransactionsHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTransactionRequest, Result<CreateTransactionResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Manipula a solicitação de criação de uma nova transação.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados da nova transação.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `CreateTransactionResponse` se a transação for criada com sucesso,
    /// ou uma mensagem de erro em caso de falha (por exemplo, usuário ou categoria não encontrados, ou erro no banco de dados).
    /// </returns>
    public async Task<Result<CreateTransactionResponse>> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var categorie = await _unitOfWork.CategoriesRepository.FindOneAsync(categorie => categorie.Id == request.CategorieId, cancellationToken);

        if (categorie is null)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.CategoriesCategorieNotFoundMessage);

        var entity = ConvertToAggregate(request, user, categorie);

        _ = await _unitOfWork.TransactionsRepository.AddAsync(entity, cancellationToken);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<CreateTransactionResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<CreateTransactionResponse>.Success(HttpStatusCode.Created, new CreateTransactionResponse(entity.Id));
    }

    /// <summary>
    /// Converte uma solicitação `CreateTransactionRequest` em uma entidade `Transactions`.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados da nova transação.</param>
    /// <param name="user">O usuário associado à transação.</param>
    /// <param name="categorie">A categoria associada à transação.</param>
    /// <returns>A entidade `Transactions` convertida.</returns>
    private static Domain.Entities.Transactions ConvertToAggregate(CreateTransactionRequest request, Domain.Entities.Users user, Domain.Entities.Categories categorie)
    {
        return new(
            categorie,
            user,
            new TransactionValueObject(
                request.Title,
                request.Amount,
                request.TransactionType,
                request.PaymentStatus),
            DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            DateTime.SpecifyKind(request.PaymentDate, DateTimeKind.Utc));
    }
}