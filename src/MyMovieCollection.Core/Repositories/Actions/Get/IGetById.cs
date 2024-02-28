namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetById<T, TId>
{
    public Task<T?> GetByIdAsync(TId id);
}