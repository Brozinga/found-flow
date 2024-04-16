using System.ComponentModel.DataAnnotations;

namespace FoundFlow.Shared.Extensions;

public static class ValidationExtension
{
    public static IDictionary<string, string[]> ConvertToDictionary(IEnumerable<ValidationResult> validationResults)
    {
        IDictionary<string, string[]> errorsDictionary = new Dictionary<string, string[]>();

        foreach (var validationResult in validationResults)
        {
            if (validationResult.MemberNames.Any())
            {
                foreach (string memberName in validationResult.MemberNames)
                {
                    if (errorsDictionary.ContainsKey(memberName))
                    {
                        // Se já existirem erros para este membro, adiciona o novo ao final
                        var errorsList = errorsDictionary[memberName].ToList();
                        errorsList.Add(validationResult.ErrorMessage ?? string.Empty);
                        errorsDictionary[memberName] = errorsList.ToArray();
                    }
                    else
                    {
                        // Se não existirem erros para este membro, cria uma nova lista de erros
                        errorsDictionary[memberName] = new[] { validationResult.ErrorMessage ?? string.Empty };
                    }
                }
            }
            else
            {
                errorsDictionary.Add(nameof(validationResult), new string[] { validationResult.ErrorMessage! });
            }
        }

        return errorsDictionary;
    }
}