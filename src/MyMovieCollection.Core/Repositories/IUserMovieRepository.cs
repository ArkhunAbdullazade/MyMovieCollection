using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions;

namespace MyMovieCollection.Core.Repositories;

public interface IUserMovieRepository : ICreate<UserMovie>
{
    public Task<UserMovie?> GetByUserAndMovieIdAsync(string userId, int movieId);
    public Task<bool> DeleteForUserByMovieIdAsync(string? userId, int movieId);
    public Task<int> DeleteAllForUserAsync(string? userId);
    public Task<IEnumerable<UserMovie>> GetAllByMovieIdAsync(int id);
    public Task<IEnumerable<UserMovie>> GetAllByUserIdAsync(string id);
}
