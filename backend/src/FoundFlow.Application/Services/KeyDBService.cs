using FoundFlow.Application.Interfaces;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace FoundFlow.Application.Services;

public class KeyDBService : IKeyDBService
{
    private readonly ConnectionMultiplexer _keyDb;

    public KeyDBService(string connectionString)
    {
        _keyDb = ConnectionMultiplexer.Connect(connectionString);
    }

    public async Task<string> GetValueAsync(string key)
    {
        var db = _keyDb.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task<T> GetValueAsync<T>(string key)
    {
        var db = _keyDb.GetDatabase();
        var value = await db.StringGetAsync(key);
        return value.IsNull ? default : JsonConvert.DeserializeObject<T>(value);
    }

    public async Task SetValueAsync(string key, string value)
    {
        var db = _keyDb.GetDatabase();
        await db.StringSetAsync(key, value);
    }

    public async Task SetValueAsync<T>(string key, T value)
    {
        var db = _keyDb.GetDatabase();
        await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
    }
}