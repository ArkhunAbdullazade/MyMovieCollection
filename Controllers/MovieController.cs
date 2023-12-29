using System.Data.SqlClient;
using System.Net;
using System.Text.Json;
using MyMovieCollection.Attributes.Http;
using MyMovieCollection.Controllers.Base;
using Dapper;
using MyMovieCollection.Extensions;
using MyMovieCollection.Models;

namespace MyMovieCollection.Controllers;

public class MovieController : ControllerBase
{
    private const string connectionString = "Server=localhost;Database=MyMovieCollectionDb;User Id=sa;Password=Arkhun42;";

    [HttpGet("GetAll")]
    public async Task GetMoviesAsync(HttpListenerContext context)
    {
        using var writer = new StreamWriter(context.Response.OutputStream);

        using var connection = new SqlConnection(connectionString);
        var movies = await connection.QueryAsync<Movie>("select * from Movies");

        var moviesHtml = movies.GetHtml();
        await writer.WriteLineAsync(moviesHtml);

        context.Response.ContentType = "text/html";
        context.Response.StatusCode = (int)HttpStatusCode.OK;
    }

    [HttpGet("GetById")]
    public async Task GetMovieByIdAsync(HttpListenerContext context)
    {
        var movieIdToGetObj = context.Request.QueryString["id"];

        if (movieIdToGetObj == null || int.TryParse(movieIdToGetObj, out int movieIdToGet) == false)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        using var connection = new SqlConnection(connectionString);
        var movie = await connection.QueryFirstOrDefaultAsync<Movie>(
            sql: "select top 1 * from Movies where Id = @Id",
            param: new { Id = movieIdToGet });

        if (movie is null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        using var writer = new StreamWriter(context.Response.OutputStream);
        await writer.WriteLineAsync(JsonSerializer.Serialize(movie));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;
    }

    [HttpPost("Create")]
    public async Task PostMovieAsync(HttpListenerContext context)
    {
        Movie? newMovie;
        using var writer = new StreamWriter(context.Response.OutputStream);

        try
        {
            newMovie = await context.CheckMovie();
        }
        catch (ArgumentNullException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await writer.WriteAsync(e.Message);
            return;
        }

        if (newMovie is null) return;

        using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(
            @"insert into Movies (Title, Description, ImbdScore, MetaScore) 
            values(@Title, @Description, @ImbdScore, @MetaScore)",
            param: newMovie);

        context.Response.StatusCode = (int)HttpStatusCode.Created;
    }

    [HttpDelete]
    public async Task DeleteMovieAsync(HttpListenerContext context)
    {
        var movieIdToDeleteObj = context.Request.QueryString["id"];

        if (movieIdToDeleteObj == null || int.TryParse(movieIdToDeleteObj, out int movieIdToDelete) == false)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        using var connection = new SqlConnection(connectionString);
        var deletedRowsCount = await connection.ExecuteAsync(
            @"delete Movies
            where Id = @Id",
            param: new
            {
                Id = movieIdToDelete,
            });

        if (deletedRowsCount == 0)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        context.Response.StatusCode = (int)HttpStatusCode.OK;
    }

    [HttpPut]
    public async Task PutMovieAsync(HttpListenerContext context)
    {
        var movieIdToUpdateObj = context.Request.QueryString["id"];

        if (movieIdToUpdateObj is null || int.TryParse(movieIdToUpdateObj, out int movieIdToUpdate) == false)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        var movieToUpdate = await context.CheckMovie();

        if (movieToUpdate is null) return;

        using var connection = new SqlConnection(connectionString);
        var affectedRowsCount = await connection.ExecuteAsync(
            @"update Movies
            set Title = @Title, Description = @Description, ImbdScore = @ImbdScore, MetaScore = @MetaScore
            where Id = @Id",
            param: new
            {
                Id = movieIdToUpdate,
                movieToUpdate?.Title,
                movieToUpdate?.Description,
                movieToUpdate?.ImbdScore,
                movieToUpdate?.MetaScore,
            });

        if (affectedRowsCount == 0)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        context.Response.StatusCode = (int)HttpStatusCode.OK;
    }
}