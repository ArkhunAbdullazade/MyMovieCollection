using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions;
using MyMovieCollection.Core.Repositories.Actions.Get;

namespace MyMovieCollection.Core.Repositories;

public interface IUserUserRepository : ICreate<UserUser>
{
    public Task<UserUser?> GetByFollowerAndFollowedUserIdAsync(string followerId, string followedUserId);
    public Task<IEnumerable<UserUser>> GetAllFollowedByFollowerIdAsync(string? followerId);
}
