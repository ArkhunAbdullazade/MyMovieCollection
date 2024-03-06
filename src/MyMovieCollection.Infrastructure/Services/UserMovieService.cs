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
        private readonly IUserUserRepository userUserRepository;
        private readonly IMovieRepository movieRepository;
        private readonly UserManager<User> userManager;
        public UserMovieService(IUserMovieRepository userMovieRepository, UserManager<User> userManager, IMovieRepository movieRepository, IUserUserRepository userUserRepository)
        {
            this.userMovieRepository = userMovieRepository;
            this.movieRepository = movieRepository;
            this.userManager = userManager;
            this.userUserRepository = userUserRepository;
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

            userMovie.CreationDate = DateTime.Now.ToString();

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

        public async Task<IEnumerable<UserMovie>> GetAllLastFollowedUsersMovies(string followerId)
        {
            var allFollowedUsers = await this.userUserRepository.GetAllFollowedByFollowerIdAsync(followerId);
            var allLastUserMovies = Enumerable.Empty<UserMovie>();

            foreach (var followedUser in allFollowedUsers)
            {
                if (followedUser.FollowedUserId is not null)
                {
                    allLastUserMovies = allLastUserMovies.Concat(await this.userMovieRepository.GetAllByUserIdAsync(followedUser.FollowedUserId));
                }
            }

            allLastUserMovies = allLastUserMovies.OrderByDescending(um => um.CreationDate).Take(20);

            foreach (var um in allLastUserMovies) { um.Movie = await movieRepository.GetByIdAsync(um.MovieId); }

            foreach (var um in allLastUserMovies) { um.User = await userManager.FindByIdAsync(um.UserId!); }

            return allLastUserMovies ?? Enumerable.Empty<UserMovie>();
        }
    }
}