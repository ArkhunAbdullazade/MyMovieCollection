namespace MyMovieCollection.Core.Repositories.Actions;
public interface IGetAllByMovieId<T>
{
    public Task<IEnumerable<T>> GetAllByMovieIdAsync(int movieId);
}