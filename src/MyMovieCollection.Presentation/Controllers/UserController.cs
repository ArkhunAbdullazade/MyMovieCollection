using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using System.Linq;

namespace MyMovieCollection.Presentation.Controllers;

[Authorize]
[Route("/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserMovieRepository userMovieRepository;
    private readonly IMovieRepository movieRepository;
    private readonly UserManager<User> userManager;

    public UserController(IUserMovieRepository userMovieRepository, IMovieRepository movieRepository,UserManager<User> userManager)
    {
        this.userMovieRepository = userMovieRepository;
        this.movieRepository = movieRepository;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Profile() 
    {
        var user = await this.userManager.GetUserAsync(User);
        return base.View(user);
    }

    [HttpGet]
    [Route("/[controller]/Movies")]
    public async Task<IActionResult> UserMovies() 
    {
        var ums = await this.userMovieRepository.GetAllByUserIdAsync(this.userManager.GetUserId(User)!);
        var movies = await Task.WhenAll(ums.Select(async um => await movieRepository.GetByIdAsync(um.MovieId)));
        return base.View(movies);
    }
}