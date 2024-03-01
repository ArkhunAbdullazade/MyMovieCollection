using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;

namespace MyMovieCollection.Core.Services
{
    public class UserMovieService : IUserMovieService
    {
        private readonly IUserMovieRepository userMovieRepository;
        private readonly UserManager<User> userManager;
        public UserMovieService(IUserMovieRepository userMovieRepository, UserManager<User> userManager)
        {
            this.userMovieRepository = userMovieRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UserMovie>> GetAllByMovieIdAsync(int id)
        {
            var allUserMovies = await this.userMovieRepository.GetAllByMovieIdAsync(id);
        
            foreach (var e in allUserMovies) { e.User = await userManager.FindByIdAsync(e.UserId!); }

            return allUserMovies;
        }

        public async Task AddUserMovieAsync(UserMovie userMovie)
        {
            var allUserMovies = await this.userMovieRepository.GetAllByUserIdAsync(userMovie.UserId!);
        
            if (allUserMovies.Any(um => um.MovieId == userMovie.Id)) throw new ArgumentException();

            await this.userMovieRepository.CreateAsync(userMovie);
        }
    }
}