using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Infrastructure.Data;

namespace MyMovieCollection.Infrastructure.Repositories
{
    public class WatchListSqlRepository : IWatchListRepository
    {
        private readonly MyMovieCollectionDbContext dbContext;

        public WatchListSqlRepository(MyMovieCollectionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(WatchListElement watchlistElm)
        {
            await this.dbContext.WatchList.AddAsync(watchlistElm);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteAllForUserAsync(string? userId)
        {
            var allWlElmToDelete = await this.GetAllByUserIdAsync(userId!);
            foreach (var wlElm in allWlElmToDelete)
            {
                this.dbContext.WatchList.Remove(wlElm);
            }
            await this.dbContext.SaveChangesAsync();
            return allWlElmToDelete.Count();
        }

        public async Task<bool> DeleteForUserByMovieIdAsync(string? userId, int movieId)
        {
            var umToDelete = await this.GetByUserAndMovieIdAsync(userId!, movieId);
            this.dbContext.WatchList.Remove(umToDelete!);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WatchListElement>> GetAllByUserIdAsync(string id)
        {
            return await this.dbContext.WatchList.Where(wlElm => wlElm.UserId == id).ToListAsync();
        }

        public async Task<WatchListElement?> GetByUserAndMovieIdAsync(string userId, int movieId)
        {
            return await this.dbContext.WatchList.Where(wlElm => wlElm.UserId == userId && wlElm.MovieId == movieId).FirstOrDefaultAsync();
        }
    }
}