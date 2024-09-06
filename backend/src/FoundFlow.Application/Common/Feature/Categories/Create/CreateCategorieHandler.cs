using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

/// <summary>
/// Manipulador (Handler) para a solicitação de criação de uma nova categoria (`CreateCategorieRequest`).
/// </summary>
public class CreateCategorieHandler : IRequestHandler<CreateCategorieRequest, Result<CreateCategorieResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Cria uma nova instância de `CreateCategorieHandler`.
    /// </summary>
    /// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
    public CreateCategorieHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <summary>
    /// Manipula a solicitação de criação de uma nova categoria.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados da nova categoria.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>Um resultado (`Result`) contendo a resposta `CreateCategorieResponse` se a categoria for criada com sucesso, ou uma mensagem de erro em caso de falha.</returns>
    public async Task<Result<CreateCategorieResponse>> Handle(CreateCategorieRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _unitOfWork.UsersRepository.FindOneAsync(user => user.Id == request.UserId, cancellationToken);

        if (user is null)
            Result<CreateCategorieResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.UserNotFoundMessage);

        var categorie = await _unitOfWork.CategoriesRepository
            .FindOneAsync(
                categorie =>
                    categorie.CategoryName == request.Name.ToLower() &&
                    categorie.UserId == request.UserId,
                cancellationToken);

        if (categorie is not null)
            Result<CreateCategorieResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.CategoriesCategorieIsRegisteredMessage);

        var entity = ConvertToAggregate(request, user);

        _ = await _unitOfWork.CategoriesRepository.AddAsync(entity, cancellationToken);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<CreateCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<CreateCategorieResponse>.Success(HttpStatusCode.Created, new CreateCategorieResponse(entity.Id));
    }

    /// <summary>
    /// Converte uma solicitação `CreateCategorieRequest` em uma entidade `Categories`.
    /// </summary>
    /// <param name="request">A solicitação contendo os dados da nova categoria.</param>
    /// <param name="user">O usuário associado à categoria.</param>
    /// <returns>A entidade `Categories` convertida.</returns>
    private static Domain.Entities.Categories ConvertToAggregate(CreateCategorieRequest request, Domain.Entities.Users user)
    {
        var categorie = new Domain.Entities.Categories(
            user,
            request.Name,
            request.Color,
            DateTime.UtcNow);

        return categorie;
    }
}