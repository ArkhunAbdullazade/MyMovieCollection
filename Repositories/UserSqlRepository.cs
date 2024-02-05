using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Text;

namespace MyMovieCollection.Repositories;
public class UserSqlRepository : IUserRepository
{
    private readonly SqlConnection connection;

    public UserSqlRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyMovieCollectionDb");
        this.connection = new SqlConnection(connectionString);
    }

    public async Task<int> CreateAsync(User newUser)
    {
        return await connection.ExecuteAsync(
        sql: @"insert into Users (Login, Password) 
             values(@Login, @Password)",
        param: newUser);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await connection.ExecuteAsync(
        sql: @"delete from Users where Id = @Id;", 
        param: new { Id = id });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await connection.QueryAsync<User>("select * from Users");
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await connection.QueryFirstOrDefaultAsync<User>(
        sql: "select top 1 * from Users where Id = @Id",
        param: new { Id = id });
    }

    public async Task<int> UpdateAsync(int id, User userToUpdate)
    {
        if (userToUpdate.Login != null && userToUpdate.Login != null) return 0;
        
        StringBuilder sb = new StringBuilder("update Users set ");

        var count = 0;
        foreach (var propertyInfo in userToUpdate.GetType().GetProperties()) 
        {
            if (propertyInfo.GetValue(userToUpdate) is null) continue;
            if (count != 0) sb.Append(',');
            sb.Append($"{propertyInfo.Name} = @{propertyInfo.Name}");
        }
        sb.Append(" where Id = @Id");

        return await connection.ExecuteAsync(
        sql: sb.ToString(),
        param: new 
        {
            Id = id,
            userToUpdate.Login,
            userToUpdate.Password
        });
    }
}   