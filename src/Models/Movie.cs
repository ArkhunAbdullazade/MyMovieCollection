using System.Text.Json.Serialization;

namespace MyMovieCollection.Models;

public class Movie
{
    public int Id { get; set; }
    public bool Adult { get; set; }
    public string? Title { get; set; }
    public float? Budget { get; set; }
    public string? Overview { get; set; }

    [JsonPropertyName("original_language")]
    public string? OriginalLanguage { get; set; }
    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; set; }
    [JsonPropertyName("genre_ids")]
    public List<int>? Genres { get; set; }
    [JsonPropertyName("vote_average")]
    public float? Score { get; set; }
    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }
    [JsonPropertyName("original_title")]
    public string? OriginalTitle { get; set; }
    
    private string? posterPath;
    [JsonPropertyName("poster_path")]
    public string? PosterPath 
    { 
        get => $"https://image.tmdb.org/t/p/original/{posterPath}";
        set => posterPath = value;
    }
}