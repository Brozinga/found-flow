using System;

namespace FoundFlow.Application.Common.Feature.Users.Create;

public sealed class CreateUserResponse
{
    public CreateUserResponse(Guid userId, bool succeeded = true)
    {
        Succeeded = succeeded;
        UserId = userId;
    }

    public Guid UserId { get; }
    public bool Succeeded { get; }
}
