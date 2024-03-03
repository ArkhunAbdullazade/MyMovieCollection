using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Repositories;

namespace MyMovieCollection.Controllers;

[Authorize]
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
}