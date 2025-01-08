using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebKlient.Model;

namespace WebKlient.Pages
{
    public class WipeMethodsModel : PageModel
    {
        // HttpClient-fabrik, bruges til at oprette HTTP-klienter til API-kald
        private readonly IHttpClientFactory _httpClientFactory;

        // Liste over slettemetoder, der skal vises p� siden
        public List<WipeMethod> WipeMethods { get; set; } = new List<WipeMethod>();

        // Constructor, der modtager en IHttpClientFactory for at muligg�re API-kald
        public WipeMethodsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET-handler, der henter slettemetoder fra API'et
        public async Task OnGetAsync()
        {
            try
            {
                // Opret en ny HttpClient fra fabrikken
                var client = _httpClientFactory.CreateClient("ApiClient");

                // Send en GET-foresp�rgsel til API-endpointet "WipeMethods"
                var response = await client.GetAsync("WipeMethods");

                // S�rg for, at HTTP-svaret har en successtatuskode
                response.EnsureSuccessStatusCode();

                // L�s svaret som en JSON-streng
                var json = await response.Content.ReadAsStringAsync();

                // Deserialiser JSON-strengen til en liste af WipeMethod-objekter
                WipeMethods = JsonSerializer.Deserialize<List<WipeMethod>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignorer store/sm� bogstaver i JSON-egenskabsnavne
                }) ?? new List<WipeMethod>(); // Hvis deserialisering fejler, opret en tom liste
            }
            catch (Exception ex)
            {
                // Log fejl, hvis der opst�r problemer under API-kaldet eller deserialiseringen
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
    }
}
