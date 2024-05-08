using System.Threading.Tasks;
using FoundFlow.Application.Interfaces;
using FoundFlow.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoundFlow.Application.Services;

public class ManagerDbService : IManagerDbService
{
    private readonly IMongoDatabase _database;

    public ManagerDbService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public async Task<T> GetValueAsync<T>(string collectionName, string keyField, string keyValue)
        where T : IManagerModel
    {
        var collection = _database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(keyField, keyValue);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task InsertValueAsync<T>(string collectionName, T value)
        where T : IManagerModel
    {
        var collection = _database.GetCollection<T>(collectionName);
        await collection.InsertOneAsync(value);
    }

    public async Task UpdateValueAsync<T>(string collectionName, string keyField, string keyValue, T value)
        where T : IManagerModel
    {
        var collection = _database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(keyField, keyValue);
        var options = new ReplaceOptions { IsUpsert = true };
        await collection.ReplaceOneAsync(filter, value, options);
    }

    public async Task DeleteValueAsync<T>(string collectionName, string keyField, string keyValue)
        where T : IManagerModel
    {
        var collection = _database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(keyField, keyValue);
        await collection.DeleteOneAsync(filter);
    }
}