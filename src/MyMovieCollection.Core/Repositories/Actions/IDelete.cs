namespace MyMovieCollection.Core.Repositories.Actions;
public interface IDelete<T>
{
    public Task<bool> DeleteAsync(int id);
}