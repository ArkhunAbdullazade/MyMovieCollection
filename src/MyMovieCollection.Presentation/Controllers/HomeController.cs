using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Presentation.Models;

namespace MyMovieCollection.Presentation.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly IMovieRepository movieRepository;
    private readonly IUserMovieRepository userMovieRepository;
    private readonly UserManager<User> userManager;
    public HomeController(IMovieRepository movieRepository, IUserMovieRepository userMovieRepository, UserManager<User> userManager)
    {
        this.movieRepository = movieRepository;
        this.userMovieRepository = userMovieRepository;
        this.userManager = userManager;
    }

    [Authorize]
    [Route("/Home")]
    public async Task<IActionResult> Index()
    {
        var allUserMovies = await userMovieRepository.GetAllByUserIdAsync(this.userManager.GetUserId(User)!);
        var movies = await Task.WhenAll(allUserMovies.Select(async um => await movieRepository.GetByIdAsync(um.MovieId)));

        return View(movies);
    }

    public async Task<IActionResult> Main()
    {
        ViewData["Trending"] = await movieRepository.GetAllByQueryTypeAsync(Core.Enums.TmdbQueryType.Trending);
        ViewData["Popular"] = await movieRepository.GetAllByQueryTypeAsync(Core.Enums.TmdbQueryType.Popular);
        ViewData["Upcoming"] = await movieRepository.GetAllByQueryTypeAsync(Core.Enums.TmdbQueryType.Upcoming);
        ViewData["TopRated"] = await movieRepository.GetAllByQueryTypeAsync(Core.Enums.TmdbQueryType.TopRated);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
