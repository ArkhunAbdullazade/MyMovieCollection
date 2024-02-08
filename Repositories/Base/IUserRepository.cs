using MyMovieCollection.Dtos;
using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base.Actions;

namespace MyMovieCollection.Repositories.Base;
public interface IUserRepository : ICreate<User>, IUpdate<User>, IDelete<User>, IGetAll<User>, IGetById<User>, IGetByDto<UserDto, User> { }