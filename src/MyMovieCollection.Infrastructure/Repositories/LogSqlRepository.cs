using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Infrastructure.Data;

namespace MyMovieCollection.Infrastructure.Repositories;
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