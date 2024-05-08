using System.Threading.Tasks;

namespace FoundFlow.Application.Interfaces;

public interface IManagerDbService
{
    Task<T> GetValueAsync<T>(string collectionName, string keyField, string keyValue)
        where T : IManagerModel;
    Task InsertValueAsync<T>(string collectionName, T value)
        where T : IManagerModel;
    Task UpdateValueAsync<T>(string collectionName, string keyField, string keyValue, T value)
        where T : IManagerModel;

    Task DeleteValueAsync<T>(string collectionName, string keyField, string keyValue)
        where T : IManagerModel;
}