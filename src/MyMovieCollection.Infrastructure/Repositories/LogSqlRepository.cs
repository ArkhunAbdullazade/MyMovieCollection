using Microsoft.Data.SqlClient;
using Dapper;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Models;
using Microsoft.Extensions.Configuration;

namespace MyMovieCollection.Infrastructure.Repositories;
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