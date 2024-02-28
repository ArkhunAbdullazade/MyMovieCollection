namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetByLogin<T>
{
    public Task<T?> GetByLoginAsync(string? login);
}