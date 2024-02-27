using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Discover;

namespace MyMovieCollection.Infrastructure.Repositories;
public class MovieSqlRepository : IMovieRepository
{
    private const string apiKey = "90b3ceddfc15385f8acef82371c5db57";
    private TMDbClient tmdbClient;

    public MovieSqlRepository() 
    {
        this.tmdbClient = new TMDbClient(apiKey);
    }

    public async Task<IEnumerable<Movie>> GetAllBySearchAsync(int page = 1, string? search = null)
    {
        SearchContainer<SearchMovie> result = string.IsNullOrWhiteSpace(search)
                                            ? await tmdbClient.DiscoverMoviesAsync().OrderBy(DiscoverMovieSortBy.PopularityDesc).Query(page)
                                            : await tmdbClient.SearchMovieAsync(search, page);

        IEnumerable<Movie> movies = Enumerable.Empty<Movie>();

        foreach (var movie in result.Results)
        {
            movies = movies.Append(new Movie
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                OriginalTitle = movie.OriginalTitle,
                OriginalLanguage = movie.OriginalLanguage,
                Score = (float)movie.VoteAverage,
                ReleaseDate = movie.ReleaseDate.ToString(),
                Adult = movie.Adult,
                PosterPath = movie.PosterPath is not null ? $"https://image.tmdb.org/t/p/original/{movie.PosterPath}" : null,
                BackdropPath = movie.BackdropPath is not null ? $"https://image.tmdb.org/t/p/original/{movie.BackdropPath}" : null,
            });
        }
        
        return movies ?? Enumerable.Empty<Movie>();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        var result = await tmdbClient.GetMovieAsync(id);

        var movie = new Movie()
            {
                Id = result.Id,
                Title = result.Title,
                Overview = result.Overview,
                OriginalTitle = result.OriginalTitle,
                OriginalLanguage = result.OriginalLanguage,
                Score = (float)result.VoteAverage,
                ReleaseDate = result.ReleaseDate.ToString(),
                Adult = result.Adult,
                PosterPath = result.PosterPath is not null ? $"https://image.tmdb.org/t/p/original/{result.PosterPath}" : null,
                BackdropPath = result.BackdropPath is not null ? $"https://image.tmdb.org/t/p/original/{result.BackdropPath}" : null,
            };

        return movie;
    }
} 