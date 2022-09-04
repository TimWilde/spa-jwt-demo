using FrontEnd.SPA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.SPA.Controllers;

[ApiController]
[AllowAnonymous]
[Route( "api/[controller]" )]
public class WeatherForecastController : ControllerBase
{
   private static readonly string[] Summaries =
   {
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
   };

   private readonly ILogger<WeatherForecastController> logger;

   public WeatherForecastController( ILogger<WeatherForecastController> logger )
   {
      this.logger = logger;
   }

   [HttpGet]
   public IEnumerable<Weather> Get()
   {
      return Enumerable.Range( 1, 5 ).Select( index => new Weather
         (
            DateTime.Now.AddDays( index ),
            Random.Shared.Next( -20, 55 ),
            Summaries[ Random.Shared.Next( Summaries.Length ) ]
         ) )
         .ToArray();
   }
}
