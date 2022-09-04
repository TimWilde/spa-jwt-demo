using Microsoft.OpenApi.Models;

namespace FrontEnd.SPA.Configurations;

public static class SwaggerConfiguration
{
   public static OpenApiInfo Info => new()
   {
      Version = "3.141",
      Title = "Car Log API",
      Description = "Back end API for Car Log application",
      TermsOfService = new Uri( "https://wilde.id/terms-of-service" ),
      Contact = new OpenApiContact(),
      License = new OpenApiLicense()
   };

   public static OpenApiSecurityScheme SecurityScheme => new()
   {
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey,
      Scheme = "Bearer",
      In = ParameterLocation.Header,
      Description = "JSON Web Token authentication token"
   };

   public static OpenApiSecurityRequirement SecurityRequirements => new()
   {
      {
         new OpenApiSecurityScheme
         {
            Reference = new OpenApiReference
            {
               Type = ReferenceType.SecurityScheme,
               Id = "Bearer"
            }
         },
         Array.Empty<string>()
      }
   };
}
