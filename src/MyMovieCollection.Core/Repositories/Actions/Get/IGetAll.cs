namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetAll<T>
{
    public IEnumerable<T> GetAll();
}