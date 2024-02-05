using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base;
using Microsoft.Data.SqlClient;
using Dapper;

namespace MyMovieCollection.Repositories;
public class UserSqlRepository : IUserRepository
{
    private readonly SqlConnection connection;

    public UserSqlRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyMovieCollectionDb");
        this.connection = new SqlConnection(connectionString);
    }

    public Task<int> CreateAsync(User model)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(User model)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(User model)
    {
        throw new NotImplementedException();
    }
}