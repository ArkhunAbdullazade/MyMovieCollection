using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Services;
using MyMovieCollection.Infrastructure.Data;
using MyMovieCollection.Infrastructure.Repositories;
using MyMovieCollection.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyMovieCollectionDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyMovieCollectionDb"),
        useSqlOptions => { useSqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name); }
    );
});

builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<MyMovieCollectionDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddTransient<LogMiddleware>();

builder.Services.AddScoped<IUserMovieRepository, UserMovieSqlRepository>();
builder.Services.AddScoped<IMovieRepository, MovieSqlRepository>();
builder.Services.AddScoped<ILogRepository, LogSqlRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserMovieService, UserMovieService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<LogMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main}/{id?}");

app.Run();
