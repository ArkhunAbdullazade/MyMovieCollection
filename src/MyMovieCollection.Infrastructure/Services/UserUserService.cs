using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;

namespace MyMovieCollection.Core.Services
{
    public class UserUserService : IUserUserService
    {
        private readonly IUserUserRepository userUserRepository;
        public UserUserService(IUserUserRepository userUserRepository)
        {
            this.userUserRepository = userUserRepository;
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
    }
}