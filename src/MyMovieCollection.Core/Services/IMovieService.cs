using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services;
public interface IMovieService
{
    public Task<MoviesResponse> GetAllAsync(int page = 1, string? search = null);
    public Task<Movie> GetByIdAsync(int id);
}