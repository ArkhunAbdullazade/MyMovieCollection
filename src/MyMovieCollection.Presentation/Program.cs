using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Infrastructure.Repositories;
using MyMovieCollection.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<LogMiddleware>();

builder.Services.AddScoped<IMovieRepository, MovieSqlRepository>();
builder.Services.AddScoped<IUserRepository, UserSqlRepository>();
builder.Services.AddScoped<ILogRepository, LogSqlRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
