using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoundFlow.Domain.Interfaces;

public interface IManagerModel
{
    [BsonId]
    public ObjectId Id { get; set; }
}