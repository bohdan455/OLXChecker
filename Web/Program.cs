using BLL.Services.Interfaces;
using Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContextFactory<DataContext>(options =>
{
    var config = builder.Configuration;
    options.UseSqlServer(config.GetConnectionString("Default"));
});
builder.Services.AddDbContext<DataContext>(options =>
{
    var config = builder.Configuration;
    options.UseSqlServer(config.GetConnectionString("Default"));
});

builder.Services.AddWebServices();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
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
