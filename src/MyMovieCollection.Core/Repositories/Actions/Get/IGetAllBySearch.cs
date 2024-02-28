namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetAllBySearch<T>
{
    public Task<T> GetAllBySearchAsync(int page = 1, string? search = null);
}