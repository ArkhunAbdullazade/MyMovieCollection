namespace MyMovieCollection.Repositories.Actions;
public interface ICreate<T>
{
    public Task<bool> CreateAsync(T model);
}