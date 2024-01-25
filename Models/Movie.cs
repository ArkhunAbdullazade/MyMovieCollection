namespace MyMovieCollection.Models;
public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? OriginalTitle { get; set; }
    public string? PosterUrl { get; set; }
    public string? Description { get; set; }
    public float? Budget { get; set; }
    public float? ImbdScore { get; set; }
    public int? MetaScore { get; set; }
}