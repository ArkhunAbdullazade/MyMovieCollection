namespace MyMovieCollection.Repositories.Actions;
public interface IGetAll<T>
{
    public Task<IEnumerable<T>> GetAllAsync();
}