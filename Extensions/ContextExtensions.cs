using System.Net;
using System.Text.Json;
using MyMovieCollection.Models;

namespace MyMovieCollection.Extensions
{
    public static class ContextExtensions
    {
        public static async Task<Movie?> CheckMovie(this HttpListenerContext context)
        {
            using var reader = new StreamReader(context.Request.InputStream);
            var json = await reader.ReadToEndAsync();

            if (json == "{}") throw new ArgumentNullException("movie");

            var movie = JsonSerializer.Deserialize<Movie>(json);

            ArgumentNullException.ThrowIfNullOrEmpty(movie?.Title, "title");
            ArgumentNullException.ThrowIfNullOrEmpty(movie?.Description, "description");

            return movie;
        }
    }
}