using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // Til ReferenceHandler
using WebKlient.Data;

var builder = WebApplication.CreateBuilder(args);

// Hent ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// Tilføj services
// Registrer ApplicationDbContext til Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Konfigurer ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // Konfiguration for Identity (valgfrit)
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Tilføj HttpClient med validering af ApiUrl
var apiUrl = builder.Configuration["ApiUrl"];
if (string.IsNullOrEmpty(apiUrl))
{
    apiUrl = "http://api:5002/api/";
    Console.WriteLine("Warning: ApiUrl is not set. Using default: " + apiUrl);
}

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiUrl);
});

// Tilføj Razor Pages og MVC med JSON-konfiguration
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // Ignorer cykliske referencer
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Valgfrit: Mere læsevenligt JSON-output
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddRazorPages();

// Middleware og pipeline
var app = builder.Build();

// Middleware til fejlbehandling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Middleware til HTTPS, statiske filer, routing og sikkerhed
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowApiOrigin");
app.UseAuthentication();
app.UseAuthorization();

// Mapping af Razor Pages og API-controllere
app.MapRazorPages();
app.MapControllers();

// Start applikationen
app.Run();
