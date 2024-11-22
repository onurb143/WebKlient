using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebKlient.Models;

namespace WebKlient.Pages
{
    public class DisksModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<Disk> Disks { get; set; } = new List<Disk>();

        public DisksModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.GetAsync("disks"); // Matcher din API endpoint

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                Disks = JsonSerializer.Deserialize<List<Disk>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Disk>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
    }
}
