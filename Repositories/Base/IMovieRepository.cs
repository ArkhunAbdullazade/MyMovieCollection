using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base.Actions;

namespace MyMovieCollection.Repositories.Base
{
    public interface IMovieRepository : ICreate<Movie>, IGetAll<Movie>, IGetById<Movie> { }
}