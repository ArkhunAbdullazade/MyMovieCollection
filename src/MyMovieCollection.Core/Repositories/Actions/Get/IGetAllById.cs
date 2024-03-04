namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetAllById<T, TId>
{
    public Task<IEnumerable<T>> GetAllByIdAsync(TId id);
}