using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Models;

namespace MyMovieCollection.Data;
public class MyMovieCollectionDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<UserMovie> UsersMovies { get; set; }
    
    public MyMovieCollectionDbContext(DbContextOptions<MyMovieCollectionDbContext> options) : base(options) {}
}