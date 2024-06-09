using System.Collections.Generic;
using System.Threading.Tasks;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoundFlow.Infrastructure.Managers;

public class ManagerService : IManagerService
{
    private readonly IMongoDatabase _database;

    public ManagerService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public async Task<List<T>> GetListAsync<T>(string collectionName, string keyField = null, string keyValue = null)
        where T : IManagerModel
    {
        var collection = _database.GetCollection<T>(collectionName);

        if (string.IsNullOrEmpty(keyField) || string.IsNullOrEmpty(keyValue))
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        var filter = Builders<T>.Filter.Eq(keyField, keyValue);
        return await collection.Find(filter).ToListAsync();
    }

    public async Task<T> GetValueAsync<T>(string collectionName, string keyField, string keyValue)
        where T : IManagerModel
    {
        var collection = _database.GetCollection<T>(collectionName);

        if (string.IsNullOrEmpty(keyField) || string.IsNullOrEmpty(keyValue))
        {
            return await collection.Find(_ => true).FirstOrDefaultAsync();
        }

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