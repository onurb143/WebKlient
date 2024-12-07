using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using WebKlient.Model;
using System.Composition;

namespace WebKlient.Pages.SletteRapporter
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public List<WipeReport> Reports { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("WipeReport");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                Reports = JsonSerializer.Deserialize<List<WipeReport>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<WipeReport>();
            }
        }
    }
}
