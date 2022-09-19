using System.Text;
using FrontEnd.SPA.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Configurations;

public static class JwtBearerConfiguration
{
   private static IServiceProvider? services;

   private static ITokenReplayManager? ReplayManager => services!.GetService<ITokenReplayManager>();
   private static ITokenLifetimeManager? LifetimeManager => services!.GetService<ITokenLifetimeManager>();

   public static IApplicationBuilder UseJwtTokenManagement( this IApplicationBuilder builder )
   {
      services = builder.ApplicationServices;

      return builder;
   }

   public static Action<JwtBearerOptions> SetUp( WebApplicationBuilder builder ) =>
      options =>
      {
         options.TokenValidationParameters = new TokenValidationParameters
         {
            ValidIssuer = builder.Configuration[ "Jwt:Issuer" ],
            ValidAudience = builder.Configuration[ "Jwt:Audience" ],
            IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration[ "Jwt:Key" ] ) ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            LifetimeValidator = LifetimeValidator,
            TokenReplayValidator = TokenReplayValidator
         };
      };

   private static bool LifetimeValidator( DateTime? notBefore,
                                          DateTime? expires,
                                          SecurityToken securityToken,
                                          TokenValidationParameters validationParameters ) =>
      LifetimeManager?.ValidateToken( notBefore, expires, securityToken, validationParameters ) ?? false;

   private static bool TokenReplayValidator( DateTime? expirationTime,
                                             string securityToken,
                                             TokenValidationParameters validationParameters ) =>
      ReplayManager?.ValidateToken( expirationTime, securityToken, validationParameters ) ?? false;
}
