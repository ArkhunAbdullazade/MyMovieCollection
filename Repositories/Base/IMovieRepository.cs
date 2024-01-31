using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base.Actions;

namespace MyMovieCollection.Repositories.Base
{
    public interface IMovieRepository : ICreateRepository<Movie>, IGetAllRepository<Movie>, IGetByIdRepository<Movie> { }
}