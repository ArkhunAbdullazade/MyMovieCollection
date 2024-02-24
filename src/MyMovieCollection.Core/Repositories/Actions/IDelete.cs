namespace MyMovieCollection.Core.Repositories.Actions;
public interface IDelete<T, TId>
{
    public Task<bool> DeleteAsync(TId id);
}