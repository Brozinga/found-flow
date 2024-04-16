using FoundFlow.Domain.Validations;
using Microsoft.Extensions.Configuration;

namespace FoundFlow.Application.Interfaces;

public interface IRequest<out T>
    where T : class
{
    public T ConvertToEntity();
    public ValidationModel Validate(IConfiguration configuration);
}