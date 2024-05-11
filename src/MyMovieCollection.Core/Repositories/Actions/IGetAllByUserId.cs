namespace MyMovieCollection.Core.Repositories.Actions;
public interface IGetAllByUserId<T>
{
    public Task<IEnumerable<T>> GetAllByUserIdAsync(int userId);
}