using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundFlow.Domain.Interfaces;

public interface IManagerService
{
    Task<T> GetValueAsync<T>(string collectionName, string keyField, string keyValue)
        where T : IManagerModel;

    Task<List<T>> GetListAsync<T>(string collectionName, string keyField = null, string keyValue = null)
        where T : IManagerModel;

    Task InsertValueAsync<T>(string collectionName, T value)
        where T : IManagerModel;
    Task UpdateValueAsync<T>(string collectionName, string keyField, string keyValue, T value)
        where T : IManagerModel;

    Task DeleteValueAsync<T>(string collectionName, string keyField, string keyValue)
        where T : IManagerModel;
}