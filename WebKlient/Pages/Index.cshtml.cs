using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebKlient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Egenskab til at holde disk-data
        public List<Disk> Disks { get; private set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                // Hent data fra API
                var response = await _httpClient.GetAsync("http://localhost:7243/api/Disks"); // Justér URL, hvis nødvendigt
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Disks = JsonSerializer.Deserialize<List<Disk>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    // Log fejl, hvis API svarer med fejl
                    Disks = new List<Disk>(); // Tom liste ved fejl
                }
            }
            catch
            {
                Disks = new List<Disk>(); // Tom liste ved undtagelse
            }
        }

        // Model for en disk (kan flyttes til sin egen fil, hvis nødvendigt)
        public class Disk
        {
            public int DiskID { get; set; }
            public string Type { get; set; }
            public int Capacity { get; set; }
            public string Status { get; set; }
            public string Path { get; set; }
            public string SerialNumber { get; set; }
            public string Manufacturer { get; set; }
        }

    }
}
