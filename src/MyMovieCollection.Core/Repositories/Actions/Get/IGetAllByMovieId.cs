namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetAllByMovieId<T>
{
    public Task<IEnumerable<T>> GetAllByMovieIdAsync(int movieId);
}