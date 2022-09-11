namespace FrontEnd.SPA.Models;

public class JwtSettings
{
   public const string SectionName = "Jwt";

   public string? Issuer { get; init; }
   public string? Audience { get; init; }
   public string? Key { get; init; }
}
