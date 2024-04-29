using FoundFlow.Domain.Entities;
using HotChocolate.Data.Sorting;

namespace FoundFlow.Infrastructure.Database.SortTypes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1501:Avoid excessive inheritance")]
public class CategoriesSortInputType : SortInputType<Categories>
{
    protected override void Configure(ISortInputTypeDescriptor<Categories> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.Id);
        descriptor.Field(t => t.CategoryName);
        descriptor.Field(t => t.Color);
        descriptor.Field(t => t.CreationDate);
    }
}