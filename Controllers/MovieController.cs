using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Dtos;
using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base;

namespace MyMovieCollection.Controllers;

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
    [ActionName("About")]
    [Route("/Movie")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await repository.GetByIdAsync(id);

        if (movie is null) return NotFound($"Movie with id {id} doesn't exist");

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
            MetaScore = movie.MetaScore,
            ReleaseDate = movie.ReleaseDate,
        };

        bool isSuccessful = await repository.CreateAsync(newMovie) > 0;

        if (!isSuccessful) return base.StatusCode((int)HttpStatusCode.InternalServerError);

        HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

        return RedirectToAction(actionName: "Movies");
    }
}