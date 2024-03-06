using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;

namespace MyMovieCollection.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public async Task<MoviesResponse> GetAllAsync(int page = 1, string? search = null)
        {
            MoviesResponse moviesResponse;

            if (string.IsNullOrWhiteSpace(search)) moviesResponse = await movieRepository.GetAllBySearchAsync(page);
            else moviesResponse = await movieRepository.GetAllBySearchAsync(page, search);
            
            if (page > moviesResponse.TotalPages || page < 1) throw new ArgumentOutOfRangeException();

            moviesResponse.Search = search;

            return moviesResponse;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await movieRepository.GetByIdAsync(id);

            if (movie is null) throw new NullReferenceException();

            return movie;
        }
    }
}