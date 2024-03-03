namespace MyMovieCollection.Repositories.Base.Actions;
public interface IGetAll<T>
{
    public Task<IEnumerable<T>> GetAllAsync();
}