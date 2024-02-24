using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollectionю.Presentation.Dtos;

namespace MyMovieCollection.Presentation.Controllers;

[Authorize]
[Route("/[action]")]
public class UserController : Controller
{
    // private readonly IUserRepository repository;
    private readonly UserManager<User> userManager;

    public UserController(UserManager<User> userManager)
    {
        // this.repository = repository;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Profile() 
    {
        var id = userManager.GetUserId(User);
        var user = await this.userManager.FindByIdAsync(id!);
        return base.View(user);
    }   

}