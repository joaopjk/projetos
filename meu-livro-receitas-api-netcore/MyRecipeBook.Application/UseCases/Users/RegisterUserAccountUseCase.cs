using MyRecipeBook.Communication.Request;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Users;

public class RegisterUserAccountUseCase
{
    public void Execute(RequestRegisterUserAccount request)
    {
        var validator = new RegisterUserAccountValidator();
        var validationResult = validator.Validate(request);
        
        if (!validationResult.IsValid)
            throw new ErrorOnValidationException(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList());
    }
}