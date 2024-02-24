namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetByUserAndMovieId<T>
{
    public Task<T?> GetByUserAndMovieIdAsync(string userId, int movieId);
}