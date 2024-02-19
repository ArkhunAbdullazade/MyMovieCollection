namespace MyMovieCollection.Core.Repositories.Actions;
public interface IDelete<T>
{
    public Task<int> DeleteAsync(int id);
}