using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebKlient.DTO;

namespace WebKlient.Pages
{
    public class WipeReportModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<WipeReportReadDto> WipeReports { get; set; } = new List<WipeReportReadDto>();

        public WipeReportModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.GetAsync("wipereports");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                WipeReports = JsonSerializer.Deserialize<List<WipeReportReadDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<WipeReportReadDto>();
            }
            catch (Exception ex)
            {
                // Log fejl og vis venlig fejlmeddelelse til bruger
                Console.WriteLine($"Error fetching data: {ex.Message}");
                ModelState.AddModelError("", "Der opstod en fejl ved hentning af data.");
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");

                // Send DELETE-anmodning til API'et for at slette den valgte rapport
                var response = await client.DeleteAsync($"wipereports/{id}");
                if (response.IsSuccessStatusCode)
                {
                    // Opdater WipeReports listen, så den er opdateret med den nyeste data
                    await OnGetAsync();
                    return RedirectToPage(); // Redirect til samme side for at opdatere UI
                }
                else
                {
                    // Hvis delete anmodningen mislykkes, vis en fejlmeddelelse
                    ModelState.AddModelError("", "Der opstod en fejl ved sletning af rapporten.");
                }
            }
            catch (Exception ex)
            {
                // Håndter fejl og log dem
                Console.WriteLine($"Error deleting report: {ex.Message}");
                ModelState.AddModelError("", "Der opstod en teknisk fejl ved sletning af rapporten.");
            }

            return Page(); // Retur til samme side, hvis der opstår en fejl
        }
    }
}
