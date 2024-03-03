using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Repositories;

namespace MyMovieCollection.Controllers;

[Authorize]
[Route("/[action]")]
public class UserController : Controller
{
    private readonly IUserRepository repository;

    public UserController(IUserRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Profile() 
    {
        var id = int.Parse(User.FindFirstValue("UserId")!);
        var user = await repository.GetByIdAsync(id);
        return base.View(user);
    }   

}