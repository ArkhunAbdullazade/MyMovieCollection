using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories.Actions;
using MyMovieCollection.Core.Repositories.Actions.Delete;
using MyMovieCollection.Core.Repositories.Actions.Get;

namespace MyMovieCollection.Core.Repositories;

public interface IUserMovieRepository : IGetByUserAndMovieId<UserMovie>, IGetAllByMovieId<UserMovie>, IGetAllByUserId<UserMovie>, ICreate<UserMovie>, IDeleteAllForUser, IDeleteForUserByMovieId { }
