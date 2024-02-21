using Microsoft.AspNetCore.Authentication.Cookies;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Infrastructure.Repositories;
using MyMovieCollection.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
        {
            options.LoginPath = "/Identity/Login";
            options.ReturnUrlParameter = "returnUrl";
        }
    );

builder.Services.AddAuthorization();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main}/{id?}");

app.Run();
