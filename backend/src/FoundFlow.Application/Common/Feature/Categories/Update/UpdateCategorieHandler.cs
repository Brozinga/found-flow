using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Categories.Update;

public class UpdateCategorieHandler : IRequestHandler<UpdateCategorieRequest, Result<UpdateCategorieResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategorieHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    private Domain.Entities.Categories ConvertToAgreggate(UpdateCategorieRequest request, Domain.Entities.Categories categorieData, Domain.Entities.Users user)
    {
        var categorie = new Domain.Entities.Categories(
            request.Id,
            user,
            request.Name,
            request.Color,
            categorieData.CreationDate);

        return categorie;
    }

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

        if(categorie is null)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.NotFound, ErrorMessages.CategoriesCategorieNotFoundMessage);

        bool categorieExists = categoriesList.Exists(categorie =>
            categorie.CategoryName ==
            request.Name.ToLower() && categorie.Id != request.Id);

        if (categorieExists)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.CategoriesCategorieIsRegisteredWithNameMessage);

        var entity = ConvertToAgreggate(request, categorie, user);

        _ = _unitOfWork.CategoriesRepository.Update(entity);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<UpdateCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveError);

        return Result<UpdateCategorieResponse>.Success(HttpStatusCode.NoContent, new UpdateCategorieResponse(entity.Id));
    }

}