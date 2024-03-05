namespace MyMovieCollection.Core.Repositories.Actions;
public interface IGetAll<T>
{
    public Task<IEnumerable<T>> GetAllAsync();
}