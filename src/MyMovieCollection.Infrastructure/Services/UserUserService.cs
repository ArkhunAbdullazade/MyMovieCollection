using Microsoft.AspNetCore.Identity;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;

namespace MyMovieCollection.Core.Services
{
    public class UserUserService : IUserUserService
    {
        private readonly IUserUserRepository userUserRepository;
        private readonly UserManager<User> userManager;
        public UserUserService(IUserUserRepository userUserRepository, UserManager<User> userManager)
        {
            this.userUserRepository = userUserRepository;
            this.userManager = userManager;
        }

        public async Task AddUserUserAsync(UserUser userUser)
        {            
            if(await IsFollowedAsync(userUser.FollowerId!, userUser.FollowedUserId!)) throw new NullReferenceException();
            
            await this.userUserRepository.CreateAsync(userUser);
        } 

        public async Task<bool> IsFollowedAsync(string followerId, string followedUserId)
        {
            var found = await this.userUserRepository.GetByFollowerAndFollowedUserIdAsync(followerId, followedUserId) is not null;
            
            return found;
        }

        public async Task<IEnumerable<User>> GetFollowedUsersByUserId(string userId)
        {
            var followedUserUsers = await this.userUserRepository.GetAllFollowedByFollowerIdAsync(userId);
            var followedUsers = await Task.WhenAll(followedUserUsers.Select(async um => await this.userManager.FindByIdAsync(um.FollowedUserId!)));
            return followedUsers! ?? Enumerable.Empty<User>();
        }

        public async Task<IEnumerable<User>> GetFollowersByUserId(string userId)
        {
            var followersUserUsers = await this.userUserRepository.GetAllFollowersByUserIdAsync(userId);
            var followers = await Task.WhenAll(followersUserUsers.Select(async um => await this.userManager.FindByIdAsync(um.FollowerId!)));
            return followers! ?? Enumerable.Empty<User>();
        }
    }
}