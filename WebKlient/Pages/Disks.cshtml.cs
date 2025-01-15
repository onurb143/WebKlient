using WebKlient.DTO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace WebKlient.Pages
{
    public class DisksModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<DiskReadDto> Disks { get; set; } = new List<DiskReadDto>();

        public DisksModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.GetAsync("disks");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                Disks = JsonSerializer.Deserialize<List<DiskReadDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<DiskReadDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.DeleteAsync($"disks/{id}");
                response.EnsureSuccessStatusCode();

                await OnGetAsync(); // Genindlæs listen efter sletning
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting disk: {ex.Message}");
                return StatusCode(500, "Fejl under sletning");
            }
        }
    }
}
