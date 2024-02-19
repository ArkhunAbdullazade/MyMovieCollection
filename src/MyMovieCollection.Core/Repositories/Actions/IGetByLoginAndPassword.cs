namespace MyMovieCollection.Core.Repositories.Actions;
public interface IGetByLoginAndPassword<T>
{
    public Task<T?> GetByLoginAndPassword(string? login, string? password);
}