namespace MyMovieCollection.Repositories.Base.Actions
{
    public interface IGetById<T>
    {
        public Task<T?> GetByIdAsync(int id);
    }
}