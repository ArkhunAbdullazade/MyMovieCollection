namespace MyMovieCollection.Repositories.Base.Actions;
public interface ICreate<T>
{
    public Task<int> CreateAsync(T model);
}