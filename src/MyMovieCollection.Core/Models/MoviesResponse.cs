using System.Text.Json.Serialization;

namespace MyMovieCollection.Core.Models;
public class MoviesResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public IEnumerable<Movie>? Results { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
    public string? Search { get; set; }
}