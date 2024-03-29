using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieCollection.Core.Models;
public class WatchListElement
{
    public int Id { get; set; }
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public virtual User? User { get; set; }
    public int MovieId { get; set; }
}
