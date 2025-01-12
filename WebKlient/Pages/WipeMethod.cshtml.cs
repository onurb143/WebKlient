using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebKlient.DTO;

namespace WebKlient.Pages
{
    public class WipeMethodsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Liste over slettemetoder, der skal vises på siden
        public List<WipeMethodReadDto> WipeMethods { get; set; } = new List<WipeMethodReadDto>();

        // Den metode, der bliver redigeret (Kan fjernes helt nu, da vi ikke redigerer)
        [BindProperty]
        public WipeMethodReadDto WipeMethodToEdit { get; set; }

        // Constructor, med afhængighed til IHttpClientFactory for API-kald
        public WipeMethodsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Henter data om sletningsmetoder fra API'et
        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");

                // Sender GET-forespørgsel til API-endpointet "WipeMethods"
                var response = await client.GetAsync("wipemethods");

                // Sørg for, at HTTP-svaret har en successstatuskode
                response.EnsureSuccessStatusCode();

                // Læs JSON som en string
                var json = await response.Content.ReadAsStringAsync();

                // Deserialiser JSON-strengen til en liste af WipeMethodReadDto-objekter
                WipeMethods = JsonSerializer.Deserialize<List<WipeMethodReadDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<WipeMethodReadDto>();
            }
            catch (Exception ex)
            {
                // Log fejl, hvis der opstår problemer under API-kaldet eller deserialiseringen
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }

        // Håndterer sletning af en metode
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");

                // Send DELETE-anmodning til API'et med den angivne metode ID
                var response = await client.DeleteAsync($"wipemethods/{id}");
                response.EnsureSuccessStatusCode();  // Tjek for succes

                // Opdater listen af metoder efter sletning
                await OnGetAsync();

                // Fjern den valgte metode til redigering efter sletning
                WipeMethodToEdit = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting wipe method: {ex.Message}");
            }

            return Page();
        }
    }
}
