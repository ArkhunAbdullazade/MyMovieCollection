using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Services;
using MyMovieCollectionю.Presentation.Dtos;

namespace MyMovieCollection.Presentation.Controllers;

[AllowAnonymous]
public class MovieController : Controller
{
    private readonly IMovieRepository movieRepository;
    private readonly IUserMovieRepository userMovieRepository;
    private readonly UserManager<User> userManager;
    private readonly IMovieService movieService;
    private readonly IUserMovieService userMovieService;

    public MovieController(IMovieRepository movieRepository, IUserMovieRepository userMovieRepository, UserManager<User> userManager, IMovieService movieService, IUserMovieService userMovieService)
    {
        this.movieRepository = movieRepository;
        this.userMovieRepository = userMovieRepository;
        this.userManager = userManager;
        this.movieService = movieService;
        this.userMovieService = userMovieService;
    }

    [HttpGet] 
    [ActionName("Movies")]
    [Route("/Movies")]
    public async Task<IActionResult> GetAll(int page = 1, string? search = null)
    {
        MoviesResponse? moviesResponse = null;

        try
        {
            moviesResponse = await this.movieService.GetAllAsync(page, search);
        }
        catch (Exception)
        {
            return BadRequest("There is no such page in the search you gave");
        }
        
        return View(moviesResponse);
    }

    [HttpGet]
    [ActionName("About")]
    [Route("/Movie")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        Movie? movie = null;
        try
        {
            movie = await this.movieService.GetByIdAsync(id);
        }
        catch (Exception)
        {
            return NotFound($"Movie with id {id} doesn't exist");
        }

        var allUserMovies = await this.userMovieService.GetAllByMovieIdAsync(id);

        ViewData["currUserReview"] = allUserMovies.FirstOrDefault(um => um.UserId == userManager.GetUserId(User));
        ViewData["Reviews"] = allUserMovies;

        return View(movie);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromForm] UserMovieDto userMovieDto)
    {
        var newUserMovie = new UserMovie {
            UserId = this.userManager.GetUserId(User),
            MovieId = userMovieDto.Id,
            Rating = userMovieDto.Score is not null ? float.Parse(userMovieDto.Score) : null,
            Review = userMovieDto.Review,
        };
        
        try
        {
            await this.userMovieService.AddUserMovieAsync(newUserMovie);
        }
        catch (Exception)
        {
            return BadRequest("This Movie is already in your list");
        }

        return RedirectToAction("About", new { id = userMovieDto.Id });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await this.userMovieRepository.DeleteByReviewId(id);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return RedirectToAction(controllerName: "Home", actionName: "Index");
    }
}