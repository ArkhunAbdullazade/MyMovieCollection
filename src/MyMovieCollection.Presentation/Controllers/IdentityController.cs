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

[AllowAnonymous]
[Route("/[action]")]
public class IdentityController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Login() => base.View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserDto userDto)
    {
        var user = await this.userManager.FindByNameAsync(userDto.UserName!);

        if (user is null) {
            base.ModelState.AddModelError("Bad Request", "Incorrect Login!");
            return base.View();
        }

        var result = await this.signInManager.PasswordSignInAsync(user, userDto.Password!, true, true);

        if (!result.Succeeded) {
            base.ModelState.AddModelError("Bad Request", "Incorrect Password!");
            return base.View();
        }

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
            UserName = userDto.UserName,
            PhoneNumber = userDto.PhoneNumber,
        };

        var result = await this.userManager.CreateAsync(newUser, userDto.Password!);

        if(!result.Succeeded) {
            foreach (var error in result.Errors) 
            {
                base.ModelState.AddModelError(error.Code, error.Description);
            }
            
            return base.View("Signup");
        }

        // var role = new IdentityRole {Name = "Admin"};
        // await roleManager.CreateAsync(role);
        // await userManager.AddToRoleAsync(newUser, role.Name);

        return base.RedirectToAction("Login");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await this.signInManager.SignOutAsync();
        return RedirectToAction(controllerName: "Home", actionName: "Main");
    }
}