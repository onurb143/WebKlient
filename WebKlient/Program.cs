using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebKlient.Data;

var builder = WebApplication.CreateBuilder(args);

// Kestrel-konfiguration for HTTP og HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5189); // HTTP port
    if (!builder.Environment.IsDevelopment())
    {
        options.ListenAnyIP(5199, listenOptions =>
        {
            listenOptions.UseHttps("https/aspnetapp.pfx", "Test1234!"); // HTTPS certifikat
        });
    }
});

// Konfigurer databaseforbindelse
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' ikke fundet.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Konfigurer Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Registrer HttpClient til API-kald
var apiUrl = builder.Configuration["ApiUrl"] ?? "https://api:5002/api/";
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

// Registrer Razor Pages og JSON-konfiguration
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware rækkefølge!
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Kortlægning af endpoints
app.MapRazorPages();
app.MapControllers();

app.Run();
