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
    public class UserMovieService : IUserMovieService
    {
        private readonly IUserMovieRepository userMovieRepository;
        private readonly IMovieRepository movieRepository;
        private readonly UserManager<User> userManager;
        public UserMovieService(IUserMovieRepository userMovieRepository, UserManager<User> userManager, IMovieRepository movieRepository)
        {
            this.userMovieRepository = userMovieRepository;
            this.movieRepository = movieRepository;
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

        public async Task<IEnumerable<Movie>> GetAllMoviesByUserIdAsync(string id)
        {
            var ums = await this.userMovieRepository.GetAllByUserIdAsync(id);

            if (ums.IsNullOrEmpty()) 
            {
                return Enumerable.Empty<Movie>();
            }

            var movies = await Task.WhenAll(ums.Select(async um => await movieRepository.GetByIdAsync(um.MovieId)));

            return movies!;
        }
    }
}