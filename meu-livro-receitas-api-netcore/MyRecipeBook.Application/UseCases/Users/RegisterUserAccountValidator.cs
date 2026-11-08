using FluentValidation;
using MyRecipeBook.Communication.Request;

namespace MyRecipeBook.Application.UseCases.Users;

public class RegisterUserAccountValidator : AbstractValidator<RequestRegisterUserAccount>
{
    public RegisterUserAccountValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required");
        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email is required");
        });
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}