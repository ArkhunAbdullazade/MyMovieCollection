using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieCollection.Core.Models;
public class UserMovie
{
    public int Id { get; set; }
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public virtual User? User { get; set; }
    public int MovieId { get; set; }
    public virtual Movie? Movie { get; set; }
    public string? Review { get; set; }
    public float? Rating { get; set; }
    public string? CreationDate { get; set; }
}
