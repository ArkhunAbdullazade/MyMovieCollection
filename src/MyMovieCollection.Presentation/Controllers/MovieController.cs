using System.Net;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollectionю.Presentation.Dtos;

namespace MyMovieCollection.Presentation.Controllers;

[AllowAnonymous]
public class MovieController : Controller
{
    private readonly IMovieRepository movieRepository;
    private readonly IUserMovieRepository userMovieRepository;
    private readonly UserManager<User> userManager;

    public MovieController(IMovieRepository movieRepository, IUserMovieRepository userMovieRepository, UserManager<User> userManager)
    {
        this.movieRepository = movieRepository;
        this.userMovieRepository = userMovieRepository;
        this.userManager = userManager;
    }

    [HttpGet] 
    [ActionName("Movies")]
    [Route("/Movies")]
    public async Task<IActionResult> GetAll()
    {
        var movies = await movieRepository.GetAllAsync();
        return View(movies);
    }

    [HttpGet]
    [ActionName("About")]
    [Route("/Movie")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await movieRepository.GetByIdAsync(id);

        if (movie is null) return NotFound($"Movie with id {id} doesn't exist");

        var userId = this.userManager.GetUserId(base.User);
        var allUserMovies = await this.userMovieRepository.GetAllByUserIdAsync(userId!);
        
        base.ViewData["Reviews"] = allUserMovies;
        base.ViewData["IsWatched"] = allUserMovies.Any(um => um.MovieId == id) ? "true" : "false";

        return View(movie);
    }

    [HttpGet]
    [ActionName("Review")]
    public async Task<IActionResult> GetReviewById(int movieId, string? userId = null)
    {        
        userId ??= this.userManager.GetUserId(User);

        var review = await userMovieRepository.GetByUserAndMovieIdAsync(userId!, movieId);

        base.ViewData["CurrentUser"] = await userManager.GetUserAsync(User);
        base.ViewData["CurrentMovie"] = await movieRepository.GetByIdAsync(movieId);

        return View(review);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromForm] UserMovieDto userMovieDto)
    {
        var userId = this.userManager.GetUserId(base.User);
        var allUserMovies = await this.userMovieRepository.GetAllByUserIdAsync(userId!);
        
        if (allUserMovies.Any(um => um.MovieId == userMovieDto.Id)) return BadRequest("This Movie is already in your list");

        var newUserMovie = new UserMovie {
            UserId = userId,
            MovieId = userMovieDto.Id,
            Rating = userMovieDto.Score is not null ? float.Parse(userMovieDto.Score) : null,
            Review = userMovieDto.Review,
        };
        
        await this.userMovieRepository.CreateAsync(newUserMovie);

        return RedirectToAction("About", new { id = userMovieDto.Id });
    }
}