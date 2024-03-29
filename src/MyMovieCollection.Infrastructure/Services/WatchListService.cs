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
    public class WatchListService : IWatchListService
    {
        private readonly IWatchListRepository watchListRepository;
        private readonly IUserUserRepository userUserRepository;
        private readonly IMovieRepository movieRepository;
        private readonly UserManager<User> userManager;
        public WatchListService(IWatchListRepository watchListRepository, UserManager<User> userManager, IMovieRepository movieRepository, IUserUserRepository userUserRepository)
        {
            this.watchListRepository = watchListRepository;
            this.movieRepository = movieRepository;
            this.userManager = userManager;
            this.userUserRepository = userUserRepository;
        }

        public async Task AddWatchListElementAsync(WatchListElement wlElm)
        {
            var allWlElms = await this.watchListRepository.GetAllByUserIdAsync(wlElm.UserId!);
        
            if (allWlElms.Any(e => e.Id == wlElm.Id)) throw new ArgumentException();

            await this.watchListRepository.CreateAsync(wlElm);       
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesByUserIdAsync(string id)
        {
            var ums = await this.watchListRepository.GetAllByUserIdAsync(id);

            if (ums.IsNullOrEmpty()) 
            {
                return Enumerable.Empty<Movie>();
            }

            var movies = await Task.WhenAll(ums.Select(async um => await movieRepository.GetByIdAsync(um.MovieId)));

            return movies!;        
        }
    }
}