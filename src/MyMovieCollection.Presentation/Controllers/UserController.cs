using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public UserController(UserManager<User> userManager, IUserService userService, IUserMovieService userMovieService)
    {
        this.userManager = userManager;
        this.userService = userService;
        this.userMovieService = userMovieService;
    }

    [HttpGet]
    public async Task<IActionResult> Profile() 
    {
        var user = await this.userManager.GetUserAsync(User);
        return base.View(user);
    }

    // [HttpGet]
    // [Route("/[controller]")]
    // public async Task<IActionResult> GetUserById(string id) 
    // {
    //     User? user = null;
    //     try
    //     {
    //         user = await userService.GetUserByIdAsync(id);
    //     }
    //     catch (Exception)
    //     {
    //         return NotFound("User is Not Found");
    //     }

    //     return base.View(user);
    // }

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

    [HttpPut]
    [Consumes("application/json")]
    public async Task<IActionResult> Update([FromBody]UserUpdateDto userUpdateDto) 
    {
        if (!ModelState.IsValid)
        {
            return View("Profile");
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
            return View("Profile");
        }

        return RedirectToAction("Profile");
    }

    [HttpPost]
    public async Task<IActionResult> UploadProfilePicture(IFormFile file) {
        var user = await this.userManager.GetUserAsync(User);

        using var fileStream = await userService.UploadProfilePicture(file.FileName, user!);
        await file.CopyToAsync(fileStream);

        return base.RedirectToAction("Profile");
    }

    [HttpGet]
    [Route("/[controller]/Movies")]
    public async Task<IActionResult> UserMovies() 
    {
        var movies = await this.userMovieService.GetAllMoviesByUserIdAsync(this.userManager.GetUserId(User)!);
        return base.View(movies);
    }
}