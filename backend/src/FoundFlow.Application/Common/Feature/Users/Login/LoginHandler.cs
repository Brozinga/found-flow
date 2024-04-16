using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Extensions;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UsersRepository
            .FindOneAsync(
                user =>
                user.Email ==
                request.Email,
                cancellationToken);

        if (user == null)
            Result<LoginResponse>.Failure(HttpStatusCode.NotFound, "Usuario não encontrado.");

        bool isPasswordValid = Crypto.Verify(request.Password, user!.Password);

        if (!isPasswordValid)
            Result<LoginResponse>.Failure(HttpStatusCode.BadRequest, "Usuario ou senha inválidos.");

        (string token, DateTime? expires) = _tokenService.Generate(user);
        LoginResponse response = new(token, expires);
        return Result<LoginResponse>.Success(response);
    }
}