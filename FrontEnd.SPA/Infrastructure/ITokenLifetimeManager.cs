using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Infrastructure;

public interface ITokenLifetimeManager
{
   bool ValidateTokenLifetime( DateTime? notBefore,
                               DateTime? expires,
                               SecurityToken securityToken,
                               TokenValidationParameters validationParameters );

   void SignOut( SecurityToken securityToken );
}
