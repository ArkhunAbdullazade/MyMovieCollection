namespace MyMovieCollection.Core.Repositories.Actions;
public interface IGetByUserAndMovieId<T>
{
    public Task<T?> GetByUserAndMovieId(int userId, int movieId);
}