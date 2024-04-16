using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundFlow.Domain.Interfaces;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> GetAsync<T, TU>(string storedProcedure, TU parameters);
    Task InsertAsync<T>(string storedProcedure, T parameters);
    Task DeleteAsync<T>(string storedProcedure, T parameters);
    Task UpdateAsync<T>(string storedProcedure, T parameters);
}
