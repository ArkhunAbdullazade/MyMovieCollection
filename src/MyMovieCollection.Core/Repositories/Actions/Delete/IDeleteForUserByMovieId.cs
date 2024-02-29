namespace MyMovieCollection.Core.Repositories.Actions.Delete;
public interface IDeleteForUserByMovieId
{
    public Task<bool> DeleteForUserByMovieIdAsync(string? userId, int movieId);
}