using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using FoundFlow.Application.Interfaces;
using FoundFlow.Shared.Exceptions;
using FoundFlow.Shared.Extensions;
using ValidationException = FoundFlow.Shared.Exceptions.ValidationException;

namespace FoundFlow.Application.Models;

public class Result<T> : IResult
where T : class
{
    protected Result()
    {
    }

    protected Result(bool succeeded, T data, int status)
    {
        Succeeded = succeeded;
        Data = data;
        Status = status;
    }

    public bool Succeeded { get; set; }
    public object Data { get; set; }
    public int Status { get; set; }
    public static Result<T> Response(bool succeeded, int status, T data)
    {
        return new Result<T>(succeeded, data, status);
    }

    public static Result<T> Success(HttpStatusCode status, T result)
    {
        return new Result<T>(true, result, (int)status);
    }

    public static Result<T> Success(T result)
    {
        return new Result<T>(true, result, (int)HttpStatusCode.OK);
    }

    public static void Failure(HttpStatusCode status, string[] errors)
    {
        string[] errorMessage = errors ?? Array.Empty<string>();
        throw status switch
        {
            HttpStatusCode.BadRequest => new BadRequestException(string.Join(", ", errorMessage)),
            HttpStatusCode.NotFound => new NotFoundException(string.Join(", ", errorMessage)),
            HttpStatusCode.Forbidden => new ForbiddenAccessException(string.Join(", ", errorMessage)),
            HttpStatusCode.Unauthorized => new UnauthorizedException(string.Join(", ", errorMessage)),
            _ => new InvalidOperationException($"Unhandled HTTP status code: {status}")
        };
    }

    public static void Failure(IEnumerable<ValidationResult> errors)
    {
        IDictionary<string, string[]> validationErrors = ValidationExtension.ConvertToDictionary(errors);
        throw new ValidationException("Erro de validação", validationErrors);
    }

    public static void Failure(HttpStatusCode status, string error)
    {
        string errorMessage = error ?? string.Empty;

        throw status switch
        {
            HttpStatusCode.ServiceUnavailable => new ServiceUnavailableException(errorMessage),
            HttpStatusCode.InternalServerError => new InternalServerException(errorMessage),
            HttpStatusCode.BadRequest => new BadRequestException(errorMessage),
            HttpStatusCode.NotFound => new NotFoundException(errorMessage),
            HttpStatusCode.Forbidden => new ForbiddenAccessException(errorMessage),
            HttpStatusCode.Unauthorized => new UnauthorizedException(errorMessage),
            _ => new InvalidOperationException($"Unhandled HTTP status code: {status}")
        };
    }
}