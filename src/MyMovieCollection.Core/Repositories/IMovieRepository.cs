using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions.Get;

namespace MyMovieCollection.Core.Repositories;
public interface IMovieRepository : IGetAllAsync<Movie>, IGetById<Movie, int> { }
