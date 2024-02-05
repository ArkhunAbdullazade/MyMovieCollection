namespace MyMovieCollection.Repositories.Base.Actions;
public interface IUpdate<T>
{
    public Task<int> UpdateAsync(int id, T model);
}  