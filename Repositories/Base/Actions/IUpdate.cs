namespace MyMovieCollection.Repositories.Base.Actions;
public interface IUpdate<T>
{
    public Task<int> UpdateAsync(T model);
}