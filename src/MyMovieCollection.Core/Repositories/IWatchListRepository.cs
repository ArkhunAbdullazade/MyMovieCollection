using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions;

namespace MyMovieCollection.Core.Repositories;

public interface IWatchListRepository : ICreate<WatchListElement>
{
    public Task<WatchListElement?> GetByUserAndMovieIdAsync(string userId, int movieId);
    public Task<IEnumerable<WatchListElement>> GetAllByUserIdAsync(string id);
    public Task<bool> DeleteForUserByMovieIdAsync(string? userId, int movieId);
    public Task<int> DeleteAllForUserAsync(string? userId);
}
