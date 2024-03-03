namespace MyMovieCollection.Repositories.Actions;
public interface IUpdate<T>
{
    public Task<bool> UpdateAsync(int id, T model);
}  