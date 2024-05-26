using System;

namespace FoundFlow.Application.Common.Feature.Categories.Update;

public sealed class UpdateCategorieResponse
{
    public UpdateCategorieResponse(Guid categorieId, bool succeeded = true)
    {
        Succeeded = succeeded;
        CategorieId = categorieId;
    }

    public Guid CategorieId { get; }
    public bool Succeeded { get; }
}
