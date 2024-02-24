// using MyMovieCollection.Core.Repositories;
// using MyMovieCollection.Core.Models;
// using MyMovieCollection.Infrastructure.Data;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;

// namespace MyMovieCollection.Infrastructure.Repositories;
// public class UserSqlRepository : IUserRepository
// {
//     private readonly UserManager<User> userManager;

//     public UserSqlRepository(UserManager<User> userManager)
//     {
//         this.userManager = userManager;
//     }

//     public async Task<bool> IdentityCreateAsync(User newUser, string password)
//     {
//         return await this.userManager.CreateAsync(newUser, password);
//     }

//     public async Task<bool> DeleteAsync(string id)
//     {
//         var userToDelete = await this.GetByIdAsync(id);
//         return (await this.userManager.DeleteAsync(userToDelete!)).Succeeded;
//     }

//     public async Task<bool> UpdateAsync(string id, User userToUpdate)
//     {
//         var user = await this.GetByIdAsync(id);
//         if (user is null) return false;
//         user.UserName = userToUpdate.UserName;
//         user.Email = userToUpdate.Email;
//         user.PhoneNumber = userToUpdate.PhoneNumber;
//         return (await this.userManager.UpdateAsync(userToUpdate)).Succeeded;
//     }
//     public IEnumerable<User> GetAll() => this.userManager.Users;

//     public async Task<User?> GetByIdAsync(string id) => await this.userManager.FindByIdAsync(id);

//     public async Task<User?> GetByLoginAsync(string? login)
//     {
//         return await this.userManager.FindByNameAsync(login!);
//     }
// }   