using MyMovieCollection.Core.Enums;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions.Get;

namespace MyMovieCollection.Core.Repositories;
public interface IMovieRepository : IGetAllBySearch<MoviesResponse>, IGetById<Movie, int> 
{
    public Task<IEnumerable<Movie>> GetAllByQueryTypeAsync(TmdbQueryType queryType);
}
