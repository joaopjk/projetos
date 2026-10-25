using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<bool> ExistActiveUserWithEmail(string email);
}