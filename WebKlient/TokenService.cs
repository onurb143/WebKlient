using Microsoft.AspNetCore.Http;

public class TokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetToken()
    {
        // Hent JWT fra cookies
        return _httpContextAccessor.HttpContext?.Request.Cookies["jwtToken"];
    }
}
