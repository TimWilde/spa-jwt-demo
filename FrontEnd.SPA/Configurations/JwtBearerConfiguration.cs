using System.Text;
using FrontEnd.SPA.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Configurations;

public static class JwtBearerConfiguration
{
   public static Action<JwtBearerOptions> SetUp( WebApplicationBuilder builder, ITokenLifetimeManager tokenLifetimeManager ) => options =>
   {
      options.TokenValidationParameters = new TokenValidationParameters
      {
         ValidIssuer = builder.Configuration[ "Jwt:Issuer" ],
         ValidAudience = builder.Configuration[ "Jwt:Audience" ],
         IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration[ "Jwt:Key" ] ) ),
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         LifetimeValidator = tokenLifetimeManager.ValidateTokenLifetime
      };
   };
}
