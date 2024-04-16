using FoundFlow.Application.Models;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class LoginRequest : IRequest<Result<LoginResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}