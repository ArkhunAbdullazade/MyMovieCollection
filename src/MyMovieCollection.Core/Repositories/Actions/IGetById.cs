namespace MyMovieCollection.Core.Repositories.Actions;
public interface IGetById<T>
{
    public Task<T?> GetByIdAsync(int id);
}