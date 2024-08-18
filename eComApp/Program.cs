using eComApp.Components;
using eComApp.DB;
using eComApp.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Anti-Forgery service
builder.Services.AddAntiforgery();

// Add Authentication and Authorization services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Adjust this path as needed
        options.AccessDeniedPath = "/Account/AccessDenied"; // Adjust this path as needed
    });

builder.Services.AddAuthorization();

// Build configuration from appsettings files
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .Build();

// Configure DbContext
builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    options.EnableDetailedErrors();
    options.UseLazyLoadingProxies();
}, ServiceLifetime.Scoped);

builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add Authentication middleware
app.UseAuthorization();  // Add Authorization middleware

app.UseAntiforgery(); // Add Anti-forgery middleware

// Seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var seeder = new DataSeeder(dbContext);
    await seeder.SeedAsync();
}

// Map Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
