namespace MyMovieCollection.Core.Repositories.Actions;
public interface IUpdate<T, TId>
{
    public Task<bool> UpdateAsync(TId id, T model);
}  