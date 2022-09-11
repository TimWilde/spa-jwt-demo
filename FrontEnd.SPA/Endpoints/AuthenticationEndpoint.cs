using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FrontEnd.SPA.Models;
using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Endpoints;

public static class AuthenticationEndpoint
{
   public static Func<Credentials, IResult> GetToken( WebApplicationBuilder builder ) => user =>
   {
      if ( !string.Equals( "admin@wilde.id", user.Username, StringComparison.InvariantCultureIgnoreCase ) ||
           !string.Equals( "P4ssw0rd!", user.Password, StringComparison.InvariantCulture ) )
         return Results.Unauthorized();

      string issuer = builder.Configuration[ "Jwt:Issuer" ];
      string audience = builder.Configuration[ "Jwt:Audience" ];
      byte[] jwtKey = Encoding.UTF8.GetBytes( builder.Configuration[ "Jwt:Key" ] );

      var tokenDescriptor = new SecurityTokenDescriptor
      {
         Issuer = issuer,
         Audience = audience,
         Expires = DateTime.Now.AddHours( 5 ),
         Subject = new ClaimsIdentity( new[]
         {
            new Claim( JwtRegisteredClaimNames.Sub, user.Username ),
            new Claim( JwtRegisteredClaimNames.Email, user.Username )
         } ),
         SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey( jwtKey ),
            SecurityAlgorithms.HmacSha512Signature
         )
      };

      return Results.Ok( new JwtSecurityTokenHandler().CreateToken( tokenDescriptor ) );
   };
}
