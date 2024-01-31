using MyMovieCollection.Models;
using MyMovieCollection.Repositories;
using MyMovieCollection.Repositories.Base;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieRepository>(p =>
{
    var connectionString = builder.Configuration.GetConnectionString("MyMovieCollectionDb");
    return new MovieSqlRepository(new SqlConnection(connectionString));
});

var app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
