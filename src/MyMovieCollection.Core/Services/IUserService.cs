using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services;
public interface IUserService
{
    public Task<User> GetUserByIdAsync(string id);
    public Task DeleteUserAsync(string id);
    public Task<FileStream> UploadProfilePicture(string fileName, User user);
    public Task UpdateUserAsync(User user);
    public Task LoginAsync(string userName, string password);
    public Task SignupAsync(User user, string password);
    public Task SignOutAsync();
}