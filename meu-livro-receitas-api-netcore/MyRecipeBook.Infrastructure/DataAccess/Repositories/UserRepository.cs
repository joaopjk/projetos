using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Interfaces.Repositories;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UserRepository(MyRecipeBookDbContext context) : IUserRepository
{
    public async Task Add(User user) => await context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) =>
        await context.Users.AnyAsync(user => user.Email == email && user.Active);
}