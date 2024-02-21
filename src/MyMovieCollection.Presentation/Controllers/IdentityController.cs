using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollectionю.Presentation.Dtos;

namespace MyMovieCollection.Presentation.Controllers;

[AllowAnonymous]
[Route("/[action]")]
public class IdentityController : Controller
{
    private readonly IUserRepository repository;

    public IdentityController(IUserRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public IActionResult Login() => base.View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserDto userDto)
    {
        var user = await repository.GetByLoginAndPassword(userDto.Login, userDto.Password);

        if (user is null) return BadRequest("Incorrect Login or Password!");

        var claims = new Claim[]
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.Login!),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await base.HttpContext.SignInAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            principal: new ClaimsPrincipal(claimsIdentity)
        );

        return string.IsNullOrWhiteSpace(userDto.ReturnUrl)
            ? base.RedirectToAction(controllerName: "Home", actionName: "Index")
            : base.RedirectPermanent(userDto.ReturnUrl);

    }

    [HttpGet]
    public IActionResult Signup() => base.View();

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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(controllerName: "Home", actionName: "Main");
    }
}