using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Infrastructure.Data;

namespace MyMovieCollection.Infrastructure.Repositories
{
    public class UserUserSqlRepository : IUserUserRepository
    {
        private readonly MyMovieCollectionDbContext dbContext;

        public UserUserSqlRepository(MyMovieCollectionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(UserUser userUser)
        {
            await this.dbContext.UsersUsers.AddAsync(userUser);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserUser?> GetByFollowerAndFollowedUserIdAsync(string followerId, string followedUserId)
        {
            return await this.dbContext.UsersUsers.Where(um => um.FollowedUserId == followedUserId && um.FollowerId == followerId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserUser>> GetAllFollowedByFollowerIdAsync(string? followerId)
        {
            return await this.dbContext.UsersUsers.Where(um => um.FollowerId == followerId).ToListAsync();
        }

        public async Task<IEnumerable<UserUser>> GetAllFollowersByUserIdAsync(string? userId)
        {
            return await this.dbContext.UsersUsers.Where(um => um.FollowedUserId == userId).ToListAsync();
        }

        public async Task<int> DeleteUserInFollowersAndFollowed(string? userId)
        {
            var allUusToDelete = this.dbContext.UsersUsers.Where(um => um.FollowedUserId == userId || um.FollowerId == userId);

            foreach (var uu in allUusToDelete)
            {
                this.dbContext.UsersUsers.Remove(uu);
            }

            await this.dbContext.SaveChangesAsync();
            return allUusToDelete.Count();
        }
    }
}