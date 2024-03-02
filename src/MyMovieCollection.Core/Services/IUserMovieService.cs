using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services;
public interface IUserMovieService
{
    public Task<IEnumerable<UserMovie>> GetAllByMovieIdAsync(int id);
    public Task<IEnumerable<Movie>> GetAllMoviesByUserIdAsync(string id);
    public Task AddUserMovieAsync(UserMovie userMovie);

}