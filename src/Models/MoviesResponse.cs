namespace MyMovieCollection.Models;
public class MoviesResponse
{
    public int page { get; set; }
    public IEnumerable<Movie>? results { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
}