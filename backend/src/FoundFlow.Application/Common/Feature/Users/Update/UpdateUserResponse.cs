using System;

namespace FoundFlow.Application.Common.Feature.Users.Update;

public sealed class UpdateUserResponse
{
    public UpdateUserResponse(Guid userId, bool succeeded = true)
    {
        Succeeded = succeeded;
        UserId = userId;
    }

    public Guid UserId { get; }
    public bool Succeeded { get; }
}
