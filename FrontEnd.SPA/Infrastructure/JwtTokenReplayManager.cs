using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Infrastructure;

public class JwtTokenReplayManager : ITokenReplayManager
{
   private readonly IHttpContextAccessor contextAccessor;

   public JwtTokenReplayManager( IHttpContextAccessor contextAccessor )
   {
      this.contextAccessor = contextAccessor;
   }

   public bool ValidateToken( DateTime? expirationTime,
                              string securityToken,
                              TokenValidationParameters validationParameters )
   {
      if ( contextAccessor.HttpContext!.Request.Cookies.TryGetValue( "__Host-vftn", out string? fingerprint ) is false ||
           string.IsNullOrWhiteSpace( fingerprint ) )
         return false;

      string encodedFingerprint = Encode( fingerprint );

      var jwtToken = new JwtSecurityToken( securityToken );

      Claim? jtiClaim = jwtToken.Payload.Claims.FirstOrDefault( x => x.Type == JwtRegisteredClaimNames.Jti );

      return string.IsNullOrWhiteSpace( encodedFingerprint ) is false &&
             string.IsNullOrWhiteSpace( jtiClaim?.Value ) is false &&
             string.Equals( jtiClaim.Value, encodedFingerprint, StringComparison.Ordinal );
   }

   public static string Encode( string input ) =>
      Convert.ToBase64String( SHA256.HashData( Encoding.UTF8.GetBytes( input ) ) );
}
