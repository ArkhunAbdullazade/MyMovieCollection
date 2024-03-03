using MyMovieCollection.Services;
using MyMovieCollection.Models;

namespace MyMovieCollection.Repositories;
public class MovieSqlRepository : IMovieRepository
{
    private const string apiKey = "90b3ceddfc15385f8acef82371c5db57";
    private const string urlBase = "https://api.themoviedb.org/";
    private HttpClient httpClient;

    public MovieSqlRepository() 
    {
        this.httpClient = new HttpClient();
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        var queryString = $"{urlBase}{TmdbApiQueries.Trending}?api_key={apiKey}";
        var moviesResponse = await httpClient.GetFromJsonAsync<MoviesResponse>(queryString);
        return moviesResponse?.results ?? Enumerable.Empty<Movie>();
    }
    public async Task<Movie?> GetByIdAsync(int id)
    {
        var queryString = $"{urlBase}3/movie/{id}?api_key={apiKey}";
        return await httpClient.GetFromJsonAsync<Movie>(queryString);
    }
} 