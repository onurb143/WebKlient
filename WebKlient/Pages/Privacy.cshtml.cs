using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebKlient.Pages
{
    // Modellen for siden "Privacy". Håndterer logik og data for privatlivssiden.
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger; // Logger til fejlfinding og overvågning.

        // Constructor, der initialiserer loggeren via dependency injection.
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger; // Tildeler logger-instanseren til den private feltvariabel.
        }

        // Metoden, der håndterer GET-forespørgsler til siden.
        public void OnGet()
        {
            // Ingen specifik logik i denne metode for nuværende.
        }
    }
}
