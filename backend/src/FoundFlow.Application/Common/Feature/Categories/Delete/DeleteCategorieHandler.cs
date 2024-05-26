using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

public class DeleteCategorieHandler : IRequestHandler<DeleteCategorieRequest, Result<DeleteCategorieResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategorieHandler(
        IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

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
            Result<DeleteCategorieResponse>.Failure(HttpStatusCode.InternalServerError, ErrorMessages.DatabaseSaveError);

        return Result<DeleteCategorieResponse>.Success(HttpStatusCode.OK, new DeleteCategorieResponse(entity.Id));
    }

}