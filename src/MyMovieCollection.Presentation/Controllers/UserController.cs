using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Services;
using MyMovieCollectionю.Presentation.Dtos;

namespace MyMovieCollection.Presentation.Controllers;

[Authorize]
[Route("/[controller]/[action]")]
public class UserController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly IUserService userService;
    private readonly IUserMovieService userMovieService;
    private readonly IUserUserService userUserService;

    public UserController(UserManager<User> userManager, IUserService userService, IUserMovieService userMovieService, IUserUserService userUserService)
    {
        this.userManager = userManager;
        this.userService = userService;
        this.userMovieService = userMovieService;
        this.userUserService = userUserService;
    }

    [HttpGet]
    [Route("/Users")]
    public IActionResult GetAll() 
    {
        var users = this.userManager.Users.ToList();
        
        ViewData["CurrUserId"] = this.userManager.GetUserId(base.User);

        return base.View(model: users);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await this.userService.DeleteUserAsync(id);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return RedirectToAction("GetAll");
    }

    [HttpGet]
    public async Task<IActionResult> Profile(string id) 
    {
        var user = await this.userManager.FindByIdAsync(id);

        ViewData["IsFollowed"] = await this.userUserService.IsFollowedAsync(userManager.GetUserId(User)!, id);

        return base.View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Follow([FromForm] FollowDto followDto)
    {
        var newUserUser = new UserUser {
            FollowerId = userManager.GetUserId(User),
            FollowedUserId = followDto.FollowedUserId,
        };

        try
        {
            await this.userUserService.AddUserUserAsync(newUserUser);
        }
        catch (Exception)
        {
            return View("Profile", new { id = followDto.FollowedUserId });
        }

        return RedirectToAction("Profile", new { id = followDto.FollowedUserId });
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProfile() 
    {
        var user = await this.userManager.GetUserAsync(User);
        System.Console.WriteLine(user is null);
        System.Console.WriteLine(user is null);
        System.Console.WriteLine(user is null);
        System.Console.WriteLine(user is null);
        return base.View(user);
    }

    [HttpPut]
    [Consumes("application/json")]
    public async Task<IActionResult> Update([FromBody]UserUpdateDto userUpdateDto) 
    {
        if (!ModelState.IsValid)
        {
            return View("UpdateProfile");
        }
        
        var user = await this.userManager.GetUserAsync(User);
        user!.UserName = userUpdateDto.UserName;
        user.Email = userUpdateDto.Email;
        user.PhoneNumber = userUpdateDto.PhoneNumber;

        try
        {
            await userService.UpdateUserAsync(user);
        }
        catch (AggregateException exceptions)
        {
            foreach (ArgumentException error in exceptions.Flatten().InnerExceptions) 
            {
                base.ModelState.AddModelError(error.ParamName!, error.Message);
            }
            return View("UpdateProfile");
        }

        return RedirectToAction("UpdateProfile");
    }

    [HttpPost]
    public async Task<IActionResult> UploadProfilePicture(IFormFile file) {
        var user = await this.userManager.GetUserAsync(User);

        using var fileStream = await userService.UploadProfilePicture(file.FileName, user!);
        await file.CopyToAsync(fileStream);

        return base.RedirectToAction("UpdateProfile");
    }

    [HttpGet]
    [Route("/[controller]/Movies")]
    public async Task<IActionResult> UserMovies() 
    {
        var movies = await this.userMovieService.GetAllMoviesByUserIdAsync(this.userManager.GetUserId(User)!);
        return base.View(movies);
    }
}