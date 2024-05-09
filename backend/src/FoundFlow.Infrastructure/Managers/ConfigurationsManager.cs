using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoundFlow.Infrastructure.Managers;

public class ConfigurationsManager : IManagerModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public LoggingSettings LoggingSettings { get; set; }
    public SwaggerSettings SwaggerSettings { get; set; }
    public JwtSettings JwtSettings { get; set; }
    public CorsSettings CorsSettings { get; set; }
}