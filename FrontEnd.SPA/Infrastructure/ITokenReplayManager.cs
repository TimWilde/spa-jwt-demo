using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Infrastructure;

public interface ITokenReplayManager
{
   bool ValidateToken( DateTime? expirationTime, string securityToken, TokenValidationParameters validationParameters );
}
