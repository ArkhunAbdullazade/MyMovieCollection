using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Infrastructure.Data;

namespace MyMovieCollection.Infrastructure.Repositories
{
    public class UserMovieSqlRepository : IUserMovieRepository
    {
        private readonly MyMovieCollectionDbContext dbContext;

        public UserMovieSqlRepository(MyMovieCollectionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<UserMovie?> GetByUserAndMovieIdAsync(string? userId, int movieId)
        {
            return await this.dbContext.UsersMovies.Where(um => um.UserId == userId && um.MovieId == movieId).FirstOrDefaultAsync();
        }

        public async Task<UserMovie?> GetByReviewIdAsync(int id)
        {
            return await this.dbContext.UsersMovies.Where(um => um.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserMovie>> GetAllByMovieIdAsync(int movieId)
        {
            return await this.dbContext.UsersMovies.Where(um => um.MovieId == movieId).ToListAsync();
        }

        public async Task<IEnumerable<UserMovie>> GetAllByUserIdAsync(string? userId)
        {
            return await this.dbContext.UsersMovies.Where(um => um.UserId == userId).ToListAsync();
        }

        public async Task<bool> CreateAsync(UserMovie userMovie)
        {
            await this.dbContext.UsersMovies.AddAsync(userMovie);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> DeleteForUserByMovieIdAsync(string? userId, int movieId)
        {
            var umToDelete = await this.GetByUserAndMovieIdAsync(userId, movieId);
            this.dbContext.UsersMovies.Remove(umToDelete!);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteAllForUserAsync(string? userId)
        {
            var allUmsToDelete = await this.GetAllByUserIdAsync(userId);
            foreach (var um in allUmsToDelete)
            {
                this.dbContext.UsersMovies.Remove(um);
            }
            await this.dbContext.SaveChangesAsync();
            return allUmsToDelete.Count();
        }

        public async Task<bool> DeleteByReviewId(int id)
        {
            var umToDelete = await this.GetByReviewIdAsync(id);
            this.dbContext.UsersMovies.Remove(umToDelete!);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}