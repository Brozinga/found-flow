using System;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class BlockInfo
{
    public int Attempts { get; set; } = 0;
    public DateTime? BlockedSince { get; set; }
}