namespace MyMovieCollection.Repositories.Base.Actions
{
    public interface ICreateRepository<T>
    {
        public Task<int> CreateAsync(T model);
    }
}