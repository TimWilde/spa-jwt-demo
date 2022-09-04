// ReSharper disable UnusedMember.Global
// ReSharper disable NotAccessedPositionalProperty.Global

namespace FrontEnd.SPA.Models;

public record Weather( DateTime Date, int Celsius, string? Summary )
{
   public int Fahrenheit => 32 + (int) ( Celsius / 0.5556 );
}
