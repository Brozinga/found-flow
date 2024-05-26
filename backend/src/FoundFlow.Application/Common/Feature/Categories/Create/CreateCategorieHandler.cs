using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

public class CreateCategorieHandler : IRequestHandler<CreateCategorieRequest, Result<CreateCategorieResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategorieHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    private Domain.Entities.Categories ConvertToAgreggate(CreateCategorieRequest request, Domain.Entities.Users user)
    {
        var categorie = new Domain.Entities.Categories(
            user,
            request.Name,
            request.Color,
            DateTime.UtcNow);

        return categorie;
    }

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

        var entity = ConvertToAgreggate(request, user);

        _ = await _unitOfWork.CategoriesRepository.AddAsync(entity, cancellationToken);
        int isSaved = await _unitOfWork.CommitAsync(cancellationToken);

        if (isSaved <= 0)
            Result<CreateCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveErrorMessage);

        return Result<CreateCategorieResponse>.Success(HttpStatusCode.Created, new CreateCategorieResponse(entity.Id));
    }

}