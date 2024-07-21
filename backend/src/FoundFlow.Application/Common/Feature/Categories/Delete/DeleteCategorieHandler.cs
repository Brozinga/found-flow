using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

/// <summary>
/// Manipulador (Handler) para a solicitação de exclusão de uma categoria (`DeleteCategorieRequest`).
/// </summary>
public class DeleteCategorieHandler : IRequestHandler<DeleteCategorieRequest, Result<DeleteCategorieResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Cria uma nova instância de `DeleteCategorieHandler`.
    /// </summary>
    /// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
    public DeleteCategorieHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <summary>
    /// Manipula a solicitação de exclusão de uma categoria.
    /// </summary>
    /// <param name="request">A solicitação contendo o ID da categoria a ser excluída.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `DeleteCategorieResponse` se a categoria for excluída com sucesso,
    /// ou uma mensagem de erro em caso de falha (por exemplo, categoria não encontrada ou erro no banco de dados).
    /// </returns>
    public async Task<Result<DeleteCategorieResponse>> Handle(DeleteCategorieRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<DeleteCategorieResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var categorie = await _unitOfWork.CategoriesRepository
            .FindOneAsync(
                categorie =>
                    categorie.Id == request.Id,
                cancellationToken);

        if (categorie is null)
            Result<DeleteCategorieResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.CategoriesCategorieNotFoundMessage);

        var entity = _unitOfWork.CategoriesRepository.Delete(categorie);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<DeleteCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<DeleteCategorieResponse>.Success(HttpStatusCode.OK, new DeleteCategorieResponse(entity.Id));
    }
}