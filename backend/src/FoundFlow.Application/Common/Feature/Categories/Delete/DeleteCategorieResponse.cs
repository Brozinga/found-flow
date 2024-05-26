using System;

namespace FoundFlow.Application.Common.Feature.Categories.Delete;

public sealed class DeleteCategorieResponse
{
    public DeleteCategorieResponse(Guid categorieId, bool succeeded = true)
    {
        Succeeded = succeeded;
        CategorieId = categorieId;
    }

    public Guid CategorieId { get; }
    public bool Succeeded { get; }
}
