using MyMovieCollection.Data;
using MyMovieCollection.Models;

namespace MyMovieCollection.Repositories;
public class LogSqlRepository : ILogRepository
{
    private readonly MyMovieCollectionDbContext dbContext;

    public LogSqlRepository(MyMovieCollectionDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(Log newLog)
    {
        await this.dbContext.Logs.AddAsync(newLog);
        await this.dbContext.SaveChangesAsync();
        return true;
    }
}