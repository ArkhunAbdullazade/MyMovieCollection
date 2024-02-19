namespace MyMovieCollection.Core.Repositories.Actions;
public interface ICreate<T>
{
    public Task<int> CreateAsync(T model);
}