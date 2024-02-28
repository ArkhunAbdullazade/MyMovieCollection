using MyMovieCollection.Core.Enums;

namespace MyMovieCollection.Core.Repositories.Actions.Get;
public interface IGetAllByQueryType<T>
{
    public Task<IEnumerable<T>> GetAllByQueryTypeAsync(TmdbQueryType queryType);
}