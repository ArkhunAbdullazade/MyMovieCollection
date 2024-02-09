using MyMovieCollection.Models;
using MyMovieCollection.Repositories.Base;
using Microsoft.Data.SqlClient;
using Dapper;

namespace MyMovieCollection.Repositories;
public class LogSqlRepository : ILogRepository
{
    private readonly SqlConnection connection;

    public LogSqlRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyMovieCollectionDb");
        this.connection = new SqlConnection(connectionString);
    }

    public async Task<int> CreateAsync(Log newLog)
    {
        return await connection.ExecuteAsync(
        sql: @"insert into Logs (UserId, Url, MethodType, StatusCode, RequestBody, ResponseBody) 
             values (@UserId, @Url, @MethodType, @StatusCode, @RequestBody, @ResponseBody);",
        param: newLog);
    }
}