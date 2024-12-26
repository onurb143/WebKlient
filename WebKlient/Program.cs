
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

// Tilføj HttpClient
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? "http://api:5002/api/");
});

// Tilføj controllere og JSON-serialization
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Tilføj Razor Pages og MVC med JSON-konfiguration
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // Ignorer cykliske referencer
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Valgfrit: Mere læsevenligt JSON-output
        options.JsonSerializerOptions.WriteIndented = true;
    });

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers(); // Sørger for at API-controllere også bliver tilgængelige

// Start applikationen
app.Run();
