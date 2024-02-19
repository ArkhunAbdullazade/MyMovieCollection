using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollectionю.Presentation.Dtos;

namespace MyMovieCollection.Presentation.Controllers;

[Route("/[action]")]
public class IdentityController : Controller
{
    private readonly IDataProtector dataProtector;
    private readonly IUserRepository repository;

    public IdentityController(IDataProtectionProvider dataProtectionProvider, IUserRepository repository)
    {
        this.dataProtector = dataProtectionProvider.CreateProtector("MyMovieCollectionPurpose");
        this.repository = repository;
    }

    [HttpGet]
    public IActionResult Login() => base.View();

    [HttpGet]
    public IActionResult Signup() => base.View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserDto userDto)
    {
        var user = await repository.GetByLoginAndPassword(userDto.Login, userDto.Password);

        if (user is null) return BadRequest("Incorrect Login or Password!");
        
        var userHash = this.dataProtector.Protect(user.Id.ToString());

        base.HttpContext.Response.Cookies.Append("Authorize", userHash);

        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Signup([FromForm] UserDto userDto)
    {
        var newUser = new User() 
        {
            Email = userDto.Email,
            Login = userDto.Login,
            Password = userDto.Password,
        };

        await repository.CreateAsync(newUser);
        
        return base.RedirectToAction("Login");
    }
}