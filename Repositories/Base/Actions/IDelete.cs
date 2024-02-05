namespace MyMovieCollection.Repositories.Base.Actions;
public interface IDelete<T>
{
    public Task<int> DeleteAsync(T model);
}