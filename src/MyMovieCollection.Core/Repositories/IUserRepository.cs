using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions;

namespace MyMovieCollection.Core.Repositories;
public interface IUserRepository : ICreate<User>, IUpdate<User>, IDelete<User>, IGetAll<User>, IGetById<User>, IGetByLoginAndPassword<User> { }