namespace MyMovieCollection.Repositories.Base.Actions
{
    public interface IGetByIdRepository<T>
    {
        public Task<T?> GetByIdAsync(int id);
    }
}