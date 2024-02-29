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
    private readonly IUserMovieRepository userMovieRepository;
    private readonly IMovieRepository movieRepository;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    // private readonly UserService userService;

    public UserController(IUserMovieRepository userMovieRepository, IMovieRepository movieRepository,UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.userMovieRepository = userMovieRepository;
        this.movieRepository = movieRepository;
        this.userManager = userManager;
        this.signInManager = signInManager;
        // this.userService = userService;
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
        var userToDelete = await this.userManager.FindByIdAsync(id);

        await this.userMovieRepository.DeleteAllForUserAsync(id);
        await this.userManager.DeleteAsync(userToDelete!);

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

        var result = await this.userManager.UpdateAsync(user);;

        if(!result.Succeeded) {
            foreach (var error in result.Errors) 
            {
                base.ModelState.AddModelError(error.Code, error.Description);
            }

            return base.View("Profile");
        }

        await this.signInManager.RefreshSignInAsync(user);

        return RedirectToAction("Profile");
    }

    [HttpPost]
    public async Task<IActionResult> UploadProfilePicture(IFormFile file) {
        var user = await this.userManager.GetUserAsync(User);
        var userName = await this.userManager.GetUserNameAsync(user!);
        var fileExtension = new FileInfo(file.FileName).Extension;
        var filename = $"{userName}{fileExtension}";
        var destinationPath = $"wwwroot/ProfilePictures/{filename}";

        System.IO.File.Delete(destinationPath);
        using var fileStream = System.IO.File.Create(destinationPath);
        await file.CopyToAsync(fileStream);

        user!.ProfilePicture = $"/ProfilePictures/{filename}";

        await this.userManager.UpdateAsync(user);

        return base.RedirectToAction("Profile");
    }

    [HttpGet]
    [Route("/[controller]/Movies")]
    public async Task<IActionResult> UserMovies() 
    {
        var ums = await this.userMovieRepository.GetAllByUserIdAsync(this.userManager.GetUserId(User)!);
        var movies = await Task.WhenAll(ums.Select(async um => await movieRepository.GetByIdAsync(um.MovieId)));
        return base.View(movies);
    }
}