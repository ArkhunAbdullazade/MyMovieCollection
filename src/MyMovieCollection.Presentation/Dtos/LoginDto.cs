using System.ComponentModel.DataAnnotations;

namespace MyMovieCollectionю.Presentation.Dtos;
public class LoginDto
{
    [Required(ErrorMessage = "User Name cannot be empty")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "Password cannot be empty")]
    public string? Password { get; set; }
    public string? ReturnUrl { get; set; }
}