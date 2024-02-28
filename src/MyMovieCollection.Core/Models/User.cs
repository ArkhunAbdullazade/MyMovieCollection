using Microsoft.AspNetCore.Identity;

namespace MyMovieCollection.Core.Models;

public class User : IdentityUser
{
    public string? ProfilePicture { get; set; }
}