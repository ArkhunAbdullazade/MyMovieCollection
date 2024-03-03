using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Actions;

namespace MyMovieCollection.Repositories;
public interface IMovieRepository : IGetAll<Movie>, IGetById<Movie> { }
