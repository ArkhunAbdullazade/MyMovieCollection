using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Core.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(string id);
    }
}