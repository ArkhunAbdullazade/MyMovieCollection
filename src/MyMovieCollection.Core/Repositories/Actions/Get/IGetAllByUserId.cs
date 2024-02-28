namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetAllByUserId<T>
{
    public Task<IEnumerable<T>> GetAllByUserIdAsync(string userId);
}