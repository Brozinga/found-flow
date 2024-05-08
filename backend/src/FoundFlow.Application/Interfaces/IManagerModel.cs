using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FoundFlow.Application.Interfaces;

public interface IManagerModel
{
    [BsonId]
    public ObjectId Id { get; set; }
}