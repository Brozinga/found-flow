using FoundFlow.Domain.Entities;
using HotChocolate.Data.Sorting;

namespace FoundFlow.Infrastructure.Database.SortTypes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1501:Avoid excessive inheritance")]
public class TransactionsSortInputType : SortInputType<Transactions>
{
    protected override void Configure(ISortInputTypeDescriptor<Transactions> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.Id);
        descriptor.Field(t => t.CreationDate);
    }
}