using FluentValidation;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.Users;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio!");
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("O email não pode ser vazio!")
            .EmailAddress()
            .WithMessage("Formato inválido de email!");
        RuleFor(user => user.Password.Length)
            .GreaterThan(6)
            .WithMessage("O password tem que ser maior que 6 caracteres!");
    }
}