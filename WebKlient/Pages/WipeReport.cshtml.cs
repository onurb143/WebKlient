using Microsoft.AspNetCore.Mvc.RazorPages; // Til Razor Pages
using System.Text.Json; // Til JSON-serialisering og deserialisering
using WebKlient.Model; // Indeholder modellen WipeReport


namespace WebKlient.Pages
{
    public class WipeReportsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory; // Til oprettelse af HTTP-klienter

        public List<WipeReport> WipeReports { get; set; } = new List<WipeReport>(); // Liste til at gemme WipeReports-data

        // Constructor for at injicere en HttpClientFactory
        public WipeReportsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory; // Gemmer den injicerede HttpClientFactory
        }

        // Metode, der k�res, n�r siden loades med en GET-anmodning
        public async Task OnGetAsync()
        {
            try
            {
                // Opret en HTTP-klient ved hj�lp af den named client "ApiClient"
                var client = _httpClientFactory.CreateClient("ApiClient");

                // Send en GET-anmodning til API'et p� endpointet "WipeReports"
                var response = await client.GetAsync("WipeReports");

                // Sikrer, at HTTP-anmodningen lykkedes (kaster en undtagelse, hvis ikke)
                response.EnsureSuccessStatusCode();

                // L�s JSON-data fra API-svaret som en streng
                var json = await response.Content.ReadAsStringAsync();

                // Deserialiser JSON til en liste af WipeReport-objekter
                WipeReports = JsonSerializer.Deserialize<List<WipeReport>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Tillad ikke-sagsf�lsomme JSON-egenskabsnavne
                }) ?? new List<WipeReport>(); // Hvis deserialisering fejler, brug en tom liste
            }
            catch (Exception ex)
            {
                // Log fejl, hvis der opst�r en undtagelse
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
    }
}
