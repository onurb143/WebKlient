using Microsoft.AspNetCore.Mvc.RazorPages; // Tilføjer Razor Pages funktionalitet
using System.Text.Json; // Til JSON-serialisering og deserialisering
using WebKlient.Model; // Importerer disk-modellen

namespace WebKlient.Pages
{
    // Model til at håndtere data og logik for "Disks"-siden
    public class DisksModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory; // Dependency injection til at oprette HttpClient-instanser

        // Liste til at gemme diske, der hentes fra API'et
        public List<Disk> Disks { get; set; } = new List<Disk>();

        // Constructor til at initialisere HttpClientFactory
        public DisksModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Metode, der køres, når siden hentes via en GET-anmodning
        public async Task OnGetAsync()
        {
            try
            {
                // Opret en HttpClient-instans ved hjælp af navngivet klient "ApiClient"
                var client = _httpClientFactory.CreateClient("ApiClient");

                // Send en GET-anmodning til API'et for at hente diskdata
                var response = await client.GetAsync("disks"); // Matcher API-endpointet

                // Tjek, om anmodningen var succesfuld; kaster en fejl, hvis ikke
                response.EnsureSuccessStatusCode();

                // Læs JSON-indholdet fra svarmeddelelsen
                var json = await response.Content.ReadAsStringAsync();

                // Deserialiser JSON-indholdet til en liste af Disk-objekter
                Disks = JsonSerializer.Deserialize<List<Disk>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignorer forskelle mellem store og små bogstaver i JSON-egenskabsnavne
                }) ?? new List<Disk>(); // Hvis deserialisering mislykkes, opret en tom liste
            }
            catch (Exception ex)
            {
                // Log fejlmeddelelse til konsollen
                Console.WriteLine($"Fejl ved hentning af data: {ex.Message}");
            }
        }
    }
}
