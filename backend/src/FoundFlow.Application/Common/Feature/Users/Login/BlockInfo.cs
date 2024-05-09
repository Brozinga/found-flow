using System;
using FoundFlow.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class BlockInfo : IManagerModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string EmailKey { get; set; }
    public int Attempts { get; set; } = 0;
    public DateTime? BlockedSince { get; set; }
}