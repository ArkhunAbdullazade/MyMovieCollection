using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Actions;

namespace MyMovieCollection.Repositories;

public interface IUserMovieRepository : IGetByUserAndMovieId<UserMovie>, IGetAllByMovieId<UserMovie>, IGetAllByUserId<UserMovie>, ICreate<UserMovie> { }
