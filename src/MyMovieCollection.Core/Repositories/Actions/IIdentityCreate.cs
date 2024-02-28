namespace MyMovieCollection.Core.Repositories.Actions;
public interface IIdentityCreate<T>
{
    public Task<bool> IdentityCreateAsync(T user, string password);
}