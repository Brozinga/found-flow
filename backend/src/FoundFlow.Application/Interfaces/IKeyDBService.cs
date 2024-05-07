using System.Threading.Tasks;

namespace FoundFlow.Application.Interfaces;

public interface IKeyDBService
{
    Task<string> GetValueAsync(string key);
    Task<T> GetValueAsync<T>(string key);
    Task SetValueAsync(string key, string value);
    Task SetValueAsync<T>(string key, T value);
}