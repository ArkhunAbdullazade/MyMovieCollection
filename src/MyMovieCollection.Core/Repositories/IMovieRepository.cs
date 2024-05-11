using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions;

namespace MyMovieCollection.Core.Repositories;
public interface IMovieRepository : IGetAll<Movie>, IGetById<Movie> { }
