using System;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Interfaces;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Messages;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoundFlow.Application.Common.Behaviours;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenService _tokenService;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IAuthorize authorizeRequest)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"];
            if (authorizationHeader.Count == 0)
            {
                throw new UnauthorizedAccessException(ErrorMessages.TokenJwtNotSentCorrectlyMessage);
            }

            string authHeaderValue = authorizationHeader[^1];
            string[] tokenParts = authHeaderValue!.Split(" ");
            string token = tokenParts[^1];

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException(ErrorMessages.TokenJwtNotSentCorrectlyMessage);
            }

            var user = _tokenService.Read(token);

            authorizeRequest.UserId = user.UserId;
        }

        return await next();
    }
}