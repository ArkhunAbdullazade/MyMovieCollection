using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base;
using Microsoft.Data.SqlClient;
using Dapper;

namespace MyMovieCollection.Repositories
{
    public class MovieSqlRepository : IMovieRepository
    {
        private readonly SqlConnection connection;

        public MovieSqlRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyMovieCollectionDb");
            this.connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await connection.QueryAsync<Movie>("select * from Movies");
        }
        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await connection.QueryFirstOrDefaultAsync<Movie>(
            sql: "select top 1 * from Movies where Id = @Id",
            param: new { Id = id });
        }

        public async Task<int> CreateAsync(Movie newMovie)
        {
            return await connection.ExecuteAsync(
            @"insert into Movies (Title, OriginalTitle, PosterUrl, Description, Budget, ImbdScore, MetaScore, ReleaseDate) 
            values(@Title, @OriginalTitle, @PosterUrl, @Description, @Budget, @ImbdScore, @MetaScore, @ReleaseDate)", newMovie);
        }
    }
}