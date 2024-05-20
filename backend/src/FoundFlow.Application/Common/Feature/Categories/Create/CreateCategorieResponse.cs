using System;

namespace FoundFlow.Application.Common.Feature.Categories.Create;

public sealed class CreateCategorieResponse
{
    public CreateCategorieResponse(Guid categorieId, bool succeeded = true)
    {
        Succeeded = succeeded;
        CategorieId = categorieId;
    }

    public Guid CategorieId { get; }
    public bool Succeeded { get; }
}
