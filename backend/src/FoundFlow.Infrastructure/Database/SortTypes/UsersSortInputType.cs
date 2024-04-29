using FoundFlow.Domain.Entities;
using HotChocolate.Data.Sorting;

namespace FoundFlow.Infrastructure.Database.SortTypes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1501:Avoid excessive inheritance")]
public class UsersSortInputType : SortInputType<Users>
{
    protected override void Configure(ISortInputTypeDescriptor<Users> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.Id);
        descriptor.Field(t => t.CreationDate);
    }
}