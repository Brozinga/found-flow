using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ConfigurationMissingException : Exception
{
    public ConfigurationMissingException(string configurationKey)
        : base($"A propriedade '{configurationKey}' não foi encontrada no arquivo de configurações.")
    {
    }

    protected ConfigurationMissingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}