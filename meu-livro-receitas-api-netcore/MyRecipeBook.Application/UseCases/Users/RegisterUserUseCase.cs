using AutoMapper;
using MyRecipeBook.Application.Services;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Interfaces.Repositories;
using MyRecipeBook.Domain.Interfaces.UseCases;
using MyRecipeBook.Exceptions.Base;

namespace MyRecipeBook.Application.UseCases.Users;

public class RegisterUserUseCase(
    IUserRepository userRepository,
    PasswordEncripter passwordEncripter,
    IMapper mapper) : IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUser> Execute(RequestRegisterUser request)
    {
        Validate(request);

        var user = mapper.Map<User>(request);
        user.Password = passwordEncripter.Encript(request.Password);
        await userRepository.Add(user);

        return new ResponseRegisteredUser();
    }

    private static void Validate(RequestRegisterUser request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}