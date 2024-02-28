using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<User> GetUserByIdAsync(string id) {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            var user = await this.userManager.FindByIdAsync(id);
            if (user is null) throw new NullReferenceException();
            return user;
        }
    }
}