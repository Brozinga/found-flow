using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FoundFlow.Application.Models;

/// <summary>
/// Representa uma lista paginada de itens do tipo T.
/// </summary>
/// <typeparam name="T">O tipo dos itens na lista.</typeparam>
/// <remarks>
/// Cria uma nova instância.
/// </remarks>
/// <param name="items">A lista de itens da página atual.</param>
/// <param name="count">O número total de itens.</param>
/// <param name="pageNumber">O número da página atual.</param>
/// <param name="pageSize">O tamanho de cada página.</param>
[ExcludeFromCodeCoverage]
public class PaginatedList<T>(List<T> items, int count, int pageNumber, int pageSize)
{
    /// <summary>
    /// A lista de itens da página atual.
    /// </summary>
    public List<T> Items { get; } = items;

    /// <summary>
    /// O número da página atual (começando em 1).
    /// </summary>
    public int PageNumber { get; } = pageNumber;

    /// <summary>
    /// O número total de páginas.
    /// </summary>
    public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

    /// <summary>
    /// O número total de itens em todas as páginas.
    /// </summary>
    public int TotalCount { get; } = count;

    /// <summary>
    /// Indica se existe uma página anterior.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Indica se existe uma próxima página.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Cria uma instância de `PaginatedList` de forma assíncrona a partir de uma fonte de dados `IQueryable`.
    /// </summary>
    /// <param name="source">A fonte de dados dos itens.</param>
    /// <param name="pageNumber">O número da página desejada.</param>
    /// <param name="pageSize">O tamanho de cada página.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona e retorna um `PaginatedList`.</returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        int count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
