using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services;
public interface IUserUserService
{
    public Task AddUserUserAsync(UserUser userUser);
    public Task<bool> IsFollowedAsync(string followerId, string followedUserId);
    public Task<IEnumerable<User>> GetFollowedUsersByUserId(string userId);
    public Task<IEnumerable<User>> GetFollowersByUserId(string userId);
}