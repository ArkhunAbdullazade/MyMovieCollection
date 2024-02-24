namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetAllAsync<T>
{
    public Task<IEnumerable<T>> GetAllAsync();
}