using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Actions;

namespace MyMovieCollection.Repositories;
public interface IUserRepository : ICreate<User>, IUpdate<User>, IDelete<User>, IGetAll<User>, IGetById<User>, IGetByLoginAndPassword<User> { }