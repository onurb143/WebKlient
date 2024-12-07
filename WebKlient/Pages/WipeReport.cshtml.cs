using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebKlient.Model;
using WebKlient.Pages;

namespace WebKlient.Pages
{
    public class WipeReportsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<WipeReport> WipeReports { get; set; } = new List<WipeReport>();

        public WipeReportsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient"); // Brug ApiClient
                var response = await client.GetAsync("wipereports"); // Matcher API-endpointet

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                WipeReports = JsonSerializer.Deserialize<List<WipeReport>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<WipeReport>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
    }
}
