using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions;
using MyMovieCollection.Core.Repositories.Actions.Get;

namespace MyMovieCollection.Core.Repositories;
public interface IUserRepository : IIdentityCreate<User>, IUpdate<User, string>, IDelete<User, string>, IGetAll<User>, IGetById<User, string>, IGetByLogin<User> { }