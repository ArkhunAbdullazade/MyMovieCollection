using MyMovieCollection.Models;

namespace MyMovieCollection.Repositories
{
    public class UserMovieSqlRepository : IUserMovieRepository
    {
        public async Task<UserMovie?> GetByUserAndMovieId(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserMovie>> GetAllByMovieIdAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserMovie>> GetAllByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(UserMovie userMovie)
        {
            throw new NotImplementedException();
        }
    }
}