using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Infrastructure;

public class JwtTokenLifetimeManager : ITokenLifetimeManager
{
   private static readonly HashSet<string> DisavowedSignatures = new();

   public bool ValidateTokenLifetime( DateTime? notBefore,
                                      DateTime? expires,
                                      SecurityToken securityToken,
                                      TokenValidationParameters validationParameters ) =>
      securityToken is JwtSecurityToken token &&
      token.ValidTo >= DateTime.UtcNow &&
      !DisavowedSignatures.Contains( token.RawSignature );

   public void SignOut( SecurityToken securityToken )
   {
      if ( securityToken is JwtSecurityToken token )
         DisavowedSignatures.Add( token.RawSignature );
   }
}
