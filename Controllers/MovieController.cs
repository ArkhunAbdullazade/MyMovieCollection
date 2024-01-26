using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMovieCollection.Dtos;
using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base;

namespace MyMovieCollection.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository repository;

        public MovieController(IMovieRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet] 
        [ActionName("Movies")]
        [Route("/Movies")]
        public async Task<IActionResult> GetAll()
        {
            var movies = await repository.GetAllAsync();

            return View(movies);
        }

        [HttpGet]
        [Route("/Movie")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await repository.GetByIdAsync(id);

            if (movie == null) return NotFound($"Movie with id {id} doesn't exist");

            return View(movie);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieDto movie)
        {
            if (string.IsNullOrEmpty(movie.Title)) return BadRequest("Title must be filled");

            if (string.IsNullOrEmpty(movie.Description)) return BadRequest("Description must be filled");

            var newMovie = new Movie()
            {
                Title = movie.Title,
                OriginalTitle = movie.OriginalTitle,
                PosterUrl = movie.PosterUrl,
                Description = movie.Description,
                Budget = movie.Budget,
                ImbdScore = movie.ImbdScore,
                MetaScore = movie.MetaScore
            };

            if (await repository.CreateAsync(newMovie) == 0) return BadRequest();

            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

            return RedirectToAction(actionName: "Movies");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}