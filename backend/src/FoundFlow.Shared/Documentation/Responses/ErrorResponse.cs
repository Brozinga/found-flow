using Swashbuckle.AspNetCore.Annotations;

namespace FoundFlow.Shared.Documentation.Responses;
public class ErrorResponse
{
    [SwaggerSchema("Um URI de referência para o tipo de erro específico.", Nullable = false)]
    public string Type { get; set; }

    [SwaggerSchema("Um resumo do tipo de erro.", Nullable = false)]
    public string Title { get; set; }

    [SwaggerSchema("O código de status HTTP que indica o tipo geral de erro.", Nullable = false)]
    public int? Status { get; set; }

    [SwaggerSchema("Uma descrição legível por humanos, específica para o erro ocorrido.", Nullable = false)]
    public string Detail { get; set; }

    [SwaggerSchema("Um URI que identifica a origem do erro.", Nullable = false)]
    public string Instance { get; set; }

    [SwaggerSchema("Um objeto contendo a coleção de erros.", Nullable = false)]
    public Dictionary<string, string[]> Errors { get; set; }
}
