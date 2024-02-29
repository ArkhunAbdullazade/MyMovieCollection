namespace MyMovieCollection.Core.Repositories.Actions.Delete;
public interface IDeleteAllForUser
{
    public Task<int> DeleteAllForUserAsync(string? userId);
}