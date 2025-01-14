using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebKlient.Data
{

    /// Repræsenterer datakontexten for Identity Framework.
    /// Denne klasse håndterer interaktionen med databasen for brugere, roller og autentifikation.

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
 /// Konfigurerer databaseindstillinger og initialiserer konteksten.
   

        /// <param name="options"
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
