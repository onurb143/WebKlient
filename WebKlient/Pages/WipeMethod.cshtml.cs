using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebKlient.Model;

namespace WebKlient.Pages
{
    public class WipeMethodsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<WipeMethod> WipeMethods { get; set; } = new List<WipeMethod>();

        public WipeMethodsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.GetAsync("WipeMethods"); // Matcher dit API-endpoint

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                WipeMethods = JsonSerializer.Deserialize<List<WipeMethod>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<WipeMethod>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
    }
}
