using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services;
public interface IUserService
{
    public Task<User> GetUserByIdAsync(string id);
    public Task DeleteUserAsync(string id);

    public Task<FileStream> UploadProfilePicture(string fileName, User user);
}