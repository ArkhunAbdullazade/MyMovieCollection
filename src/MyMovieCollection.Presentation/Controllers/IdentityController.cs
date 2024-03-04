using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Services;
using MyMovieCollectionю.Presentation.Dtos;

namespace MyMovieCollection.Presentation.Controllers;

[AllowAnonymous]
[Route("/[action]")]
public class IdentityController : Controller
{
    private readonly IUserService userService;

    public IdentityController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public IActionResult Login() => base.View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        try
        {
            await this.userService.LoginAsync(loginDto.UserName!, loginDto.Password!);
        }
        catch (ArgumentException exception)
        {
            base.ModelState.AddModelError(exception.ParamName!, exception.Message);
            return View("Login");
        }   
        catch (NullReferenceException exception)
        {
            base.ModelState.AddModelError(exception.Source!, exception.Message);
            return View("Login");
        }

        return string.IsNullOrWhiteSpace(loginDto.ReturnUrl)
            ? base.RedirectToAction(controllerName: "Home", actionName: "Index")
            : base.RedirectPermanent(loginDto.ReturnUrl);
    }

    [HttpGet]
    public IActionResult Signup() => base.View();

    [HttpPost]
    public async Task<IActionResult> Signup([FromForm] SignupDto signupDto)
    {
        if (!ModelState.IsValid)
        {
            return View("Signup");
        }

        var newUser = new User()
        {
            Email = signupDto.Email,
            UserName = signupDto.UserName,
            PhoneNumber = signupDto.PhoneNumber,
        };
        
        try
        {
            await this.userService.SignupAsync(newUser, signupDto.Password!);
        }
        catch (AggregateException exceptions)
        {
            foreach (ArgumentException error in exceptions.Flatten().InnerExceptions) 
            {
                base.ModelState.AddModelError(error.ParamName!, error.Message);
            }
            return View("Signup");
        }

        return base.RedirectToAction("Login");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await this.userService.SignOutAsync();

        return RedirectToAction(controllerName: "Home", actionName: "Main");
    }
}