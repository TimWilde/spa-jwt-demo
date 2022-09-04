using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FrontEnd.SPA.Configurations;

public static class AuthenticationConfiguration
{
   public static Action<AuthenticationOptions> SetUpOptions => options =>
   {
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   };
}
