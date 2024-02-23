using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Data;
using MyMovieCollection.Models;

namespace MyMovieCollection.Repositories;
public class UserSqlRepository : IUserRepository
{
    private readonly MyMovieCollectionDbContext dbContext;

    public UserSqlRepository(MyMovieCollectionDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(User newUser)
    {
        await this.dbContext.Users.AddAsync(newUser);
        await this.dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userToDelete = await this.GetByIdAsync(id);
        if (userToDelete is null) return false;
        this.dbContext.Users.Remove(userToDelete);
        await this.dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(int id, User userToUpdate)
    {
        var user = await this.GetByIdAsync(id);
        if (user is null) return false;
        user.Login = userToUpdate.Login;
        user.Password = userToUpdate.Password;
        user.Email = userToUpdate.Email;
        user.PhoneNumber = userToUpdate.PhoneNumber;
        await this.dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<User>> GetAllAsync() => await this.dbContext.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(int id) => await this.dbContext.Users.FindAsync(id);

    public async Task<User?> GetByLoginAndPassword(string? login, string? password)
    {
        return await this.dbContext.Users.FirstAsync(u => u.Login == login && u.Password == password);
    }
}   