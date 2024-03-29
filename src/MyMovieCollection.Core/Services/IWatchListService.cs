using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services;
public interface IWatchListService
{
    public Task<IEnumerable<Movie>> GetAllMoviesByUserIdAsync(string id);
    public Task AddWatchListElementAsync(WatchListElement wlElm);
}