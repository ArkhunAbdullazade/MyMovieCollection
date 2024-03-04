using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services;
public interface IUserUserService
{
    public Task AddUserUserAsync(UserUser userUser);
    public Task<bool> IsFollowedAsync(string followerId, string followedUserId);
}