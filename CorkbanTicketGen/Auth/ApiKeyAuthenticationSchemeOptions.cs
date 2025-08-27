using Microsoft.AspNetCore.Authentication;

namespace CorkbanTicketGen.Auth;

public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "APIKeyAuthentication";
    public const string HeaderName = "X-Corkban-Key";
}