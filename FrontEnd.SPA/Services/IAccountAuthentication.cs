using FrontEnd.SPA.Models;

namespace FrontEnd.SPA.Services;

public interface IAccountAuthentication
{
   public bool IsValid( Credentials credentials );
}
