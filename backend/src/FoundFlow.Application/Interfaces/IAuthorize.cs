using System;
using Newtonsoft.Json;

namespace FoundFlow.Application.Interfaces;

public interface IAuthorize
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}