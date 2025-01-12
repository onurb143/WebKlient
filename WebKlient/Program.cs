using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
    UseCookies = true,
    CookieContainer = new CookieContainer(),
    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true // Accepter alle certifikater (kun til udvikling)
});




// Registrer Razor Pages og JSON-konfiguration
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddRazorPages();

// Konfigurer CORS (Cross-Origin Resource Sharing)
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new[] { "https://api:5002", "https://web:5199" };
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:5199")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});



var app = builder.Build();

// Middleware r�kkef�lge!
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();  // Kortlægging af Razor Pages direkte her
app.MapFallbackToPage("/Jwt");
app.MapControllers();

app.Run();
