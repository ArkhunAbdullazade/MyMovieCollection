namespace MyMovieCollection.Repositories.Base.Actions
{
    public interface IGetAllRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
    }
}