using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace WebKlient.Pages
{
    public class JWTModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public JWTModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public string JwtToken { get; private set; }
        public string Message { get; private set; }

        public async Task<IActionResult> OnPostFetchJwtAsync()
        {
            try
            {
                var loginPayload = new
                {
                    Email = "bruno@kock.dk",
                    Password = "Test1234!"
                };

                var loginJson = JsonSerializer.Serialize(loginPayload);
                var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

                var loginResponse = await _httpClient.PostAsync("Auth/login", loginContent);

                if (loginResponse.IsSuccessStatusCode)
                {
                    var loginResponseJson = await loginResponse.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(loginResponseJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (tokenResponse?.Token != null)
                    {
                        JwtToken = tokenResponse.Token;
                        Message = "JWT hentet med succes.";

                        // Gem JWT som cookie
                        Response.Cookies.Append("jwtToken", JwtToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = !HttpContext.Request.IsHttps,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.UtcNow.AddHours(2)
                        });

                    }
                    else
                    {
                        Message = "JWT kunne ikke hentes. Token er null.";
                    }
                }
                else
                {
                    Message = $"Kunne ikke logge ind. Statuskode: {loginResponse.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                Message = $"En fejl opstod under hentning af JWT: {ex.Message}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostFetchProtectedDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("ProtectedEndpoint");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    ViewData["ProtectedData"] = data;
                }
                else
                {
                    ViewData["Error"] = $"Kunne ikke hente data. Statuskode: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"En fejl opstod: {ex.Message}";
            }

            return Page();
        }



        public class TokenResponse
        {
            public string? Token { get; set; }
            public string? Error { get; set; }
        }
    }

}
