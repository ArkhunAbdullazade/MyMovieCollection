namespace MyMovieCollection.Core.Models;
public class UserMovie
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public string? Review { get; set; }
    public float? Rating { get; set; }
}
