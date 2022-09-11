using FrontEnd.SPA.Models;

namespace FrontEnd.SPA.Services.Implementation;

public class CredentialsCheck : IAccountAuthentication
{
   public bool IsValid( Credentials credentials ) =>
      string.Equals( credentials.Username, "string", StringComparison.InvariantCultureIgnoreCase ) &&
      string.Equals( credentials.Password, "string", StringComparison.InvariantCulture );
}
