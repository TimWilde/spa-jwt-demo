using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using FrontEnd.SPA.Infrastructure;
using FrontEnd.SPA.Models;
using FrontEnd.SPA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FrontEnd.SPA.Controllers;

[ApiController]
[AllowAnonymous]
[Route( "api/[controller]" )]
public class AuthenticationController : ControllerBase
{
   private readonly IAccountAuthentication authentication;
   private readonly JwtSettings configuration;
   private readonly ITokenLifetimeManager tokenLifetimeManager;

   public AuthenticationController( IOptions<JwtSettings> configuration,
                                    IAccountAuthentication authentication,
                                    ITokenLifetimeManager tokenLifetimeManager )
   {
      this.configuration = configuration.Value;
      this.authentication = authentication;
      this.tokenLifetimeManager = tokenLifetimeManager;
   }

   [HttpPost( "login" )]
   [ProducesResponseType( (int) HttpStatusCode.OK, Type = typeof(JwtToken) )]
   [ProducesResponseType( (int) HttpStatusCode.Unauthorized, Type = typeof(ApiResponse) )]
   public IActionResult LogIn( Credentials credentials )
   {
      if ( !authentication.IsValid( credentials ) ) return Unauthorized();

      var userFingerprint = "Something random";
      var tokenDescriptor = new SecurityTokenDescriptor
      {
         Issuer = configuration.Issuer,
         Audience = configuration.Audience,
         Expires = DateTime.Now.AddHours( 5 ),
         Subject = new ClaimsIdentity( new[]
         {
            new Claim( JwtRegisteredClaimNames.Sub, credentials.Username ),
            new Claim( JwtRegisteredClaimNames.Email, credentials.Username ),
            new Claim( JwtRegisteredClaimNames.Jti, JwtTokenReplayManager.Encode( userFingerprint ) )
         } ),
         SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey( Encoding.UTF8.GetBytes( configuration.Key ) ),
            SecurityAlgorithms.HmacSha512Signature
         )
      };

      var token = new JwtSecurityTokenHandler().CreateToken( tokenDescriptor ) as JwtSecurityToken;

      Response.Cookies.Append( "__Host-vftn",
                               userFingerprint,
                               new CookieOptions
                               {
                                  HttpOnly = true,
                                  Secure = true,
                                  SameSite = SameSiteMode.Strict,
                                  IsEssential = true
                               } );
      return Ok( new JwtToken( token!.RawData ) );
   }

   [HttpPost( "logout" )]
   [ProducesResponseType( (int) HttpStatusCode.OK )]
   public IActionResult LogOut( [FromHeader( Name = "Authorization" )] string? authorization )
   {
      if ( string.IsNullOrWhiteSpace( authorization ) ) return Ok();

      string bearerToken =
         authorization.Replace( "Bearer ", string.Empty, StringComparison.InvariantCultureIgnoreCase );

      tokenLifetimeManager.SignOut( new JwtSecurityToken( bearerToken ) );

      return Ok();
   }
}
