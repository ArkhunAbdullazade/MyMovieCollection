using System.ComponentModel.DataAnnotations;

namespace MyMovieCollectionю.Presentation.Dtos;
public class UserUpdateDto
{
    [EmailAddress]
    [Required(ErrorMessage = "Email cannot be empty")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "User Name cannot be empty")]
    public string? UserName { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
}