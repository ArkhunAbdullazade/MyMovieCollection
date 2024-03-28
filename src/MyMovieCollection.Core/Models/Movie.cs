using System.Text.Json.Serialization;

namespace MyMovieCollection.Core.Models;

public class Movie
{
    public int Id { get; set; }
    public bool Adult { get; set; }
    public string? Title { get; set; }
    public float? Budget { get; set; }
    public string? Overview { get; set; }
    public string? OriginalLanguage { get; set; }
    public float? Score { get; set; }
    public string? ReleaseDate { get; set; }
    public string? OriginalTitle { get; set; }
    public string? PosterPath { get; set; }
    public string? BackdropPath { get; set; }
    public string? Trailer { get; set; }
}