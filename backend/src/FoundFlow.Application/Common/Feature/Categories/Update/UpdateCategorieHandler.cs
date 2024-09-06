using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Categories.Update;

/// <summary>
/// Manipulador (Handler) para a solicitação de atualização de uma categoria (`UpdateCategorieRequest`).
/// </summary>
public class UpdateCategorieHandler : IRequestHandler<UpdateCategorieRequest, Result<UpdateCategorieResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Cria uma nova instância de `UpdateCategorieHandler`.
    /// </summary>
    /// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
    public UpdateCategorieHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <summary>
    /// Manipula a solicitação de atualização de uma categoria.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados da categoria a ser atualizada.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `UpdateCategorieResponse` se a categoria for atualizada com sucesso,
    /// ou uma mensagem de erro em caso de falha (por exemplo, categoria não encontrada, nome da categoria já existe para o usuário ou erro no banco de dados).
    /// </returns>
    public async Task<Result<UpdateCategorieResponse>> Handle(UpdateCategorieRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var categoriesList = await _unitOfWork.CategoriesRepository
            .FindAsync(
                categorie =>
                    categorie.UserId == request.UserId,
                cancellationToken);

        var categorie = categoriesList.Find(categorie => categorie.Id == request.Id);

        if (categorie is null)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.CategoriesCategorieNotFoundMessage);

        bool categorieExists = categoriesList.Exists(categorie =>
            categorie.CategoryName ==
            request.Name.ToLower() && categorie.Id != request.Id);

        if (categorieExists)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.CategoriesCategorieIsRegisteredWithNameMessage);

        var entity = ConvertToAggregate(request, categorie, user);

        _ = _unitOfWork.CategoriesRepository.Update(entity);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<UpdateCategorieResponse>.Success(HttpStatusCode.NoContent, new UpdateCategorieResponse(entity.Id));
    }

    /// <summary>
    /// Converte uma solicitação `UpdateCategorieRequest` em uma entidade `Categories`.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados da categoria a ser atualizada.</param>
    /// <param name="categorieData">Os dados atuais da categoria a ser atualizada.</param>
    /// <param name="user">O usuário associado à categoria.</param>
    /// <returns>A entidade `Categories` convertida e atualizada.</returns>
    private static Domain.Entities.Categories ConvertToAggregate(UpdateCategorieRequest request, Domain.Entities.Categories categorieData, Domain.Entities.Users user)
    {
        return new Domain.Entities.Categories(
            request.Id,
            user,
            request.Name,
            request.Color,
            DateTime.SpecifyKind(categorieData.CreationDate, DateTimeKind.Utc));
    }
}