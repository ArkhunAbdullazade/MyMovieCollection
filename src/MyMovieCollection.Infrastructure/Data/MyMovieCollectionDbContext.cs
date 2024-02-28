using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Core.Models;

namespace MyMovieCollection.Infrastructure.Data;
public class MyMovieCollectionDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Log> Logs { get; set; }
    public DbSet<UserMovie> UsersMovies { get; set; }
    
    public MyMovieCollectionDbContext(DbContextOptions<MyMovieCollectionDbContext> options) : base(options) {}
}