namespace MyMovieCollection.Repositories.Actions;
public interface IGetByLoginAndPassword<T>
{
    public Task<T?> GetByLoginAndPassword(string? login, string? password);
}