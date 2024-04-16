using FluentValidation;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name;

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("O email informado está incorreto");
    }
}