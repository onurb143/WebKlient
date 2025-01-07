using Microsoft.AspNetCore.Mvc; // Tilf�jer funktionalitet til at h�ndtere HTTP-anmodninger
using Microsoft.AspNetCore.Mvc.RazorPages; // Tilf�jer Razor Pages-underst�ttelse
using System.Diagnostics; // Til diagnosticering og fejlh�ndtering

namespace WebKlient.Pages
{
    // Konfigurer caching og sikkerhed for fejlsiden
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // Deaktiver caching af fejl
    [IgnoreAntiforgeryToken] // Ignorer antiforgery-token, da dette er en fejlside
    public class ErrorModel : PageModel
    {
        // Indeholder ID for den aktuelle foresp�rgsel, bruges til sporing
        public string? RequestId { get; set; }

        // Indikerer, om RequestId skal vises
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger; // Logger til fejlrapportering

        // Constructor, der initialiserer loggeren via dependency injection
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        // Metode, der k�res, n�r siden loades via en GET-anmodning
        public void OnGet()
        {
            // S�tter RequestId til den nuv�rende aktivitet eller HTTP-sporings-ID
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            // Logger fejloplysninger for bedre fejldiagnosticering
            if (!string.IsNullOrEmpty(RequestId))
            {
                _logger.LogError($"Fejl opstod under behandling af foresp�rgsel med RequestId: {RequestId}");
            }
            else
            {
                _logger.LogError("Fejl opstod, men ingen RequestId kunne spores.");
            }
        }
    }
}
