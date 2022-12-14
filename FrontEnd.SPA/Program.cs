using FrontEnd.SPA.Configurations;
using FrontEnd.SPA.Infrastructure;
using FrontEnd.SPA.Models;
using FrontEnd.SPA.Services;
using FrontEnd.SPA.Services.Implementation;
using Microsoft.AspNetCore.Authorization;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

// Configure Swagger Components
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( setup =>
{
   setup.SwaggerDoc( "v1", SwaggerConfiguration.Info );
   setup.AddSecurityDefinition( "Bearer", SwaggerConfiguration.SecurityScheme );
   setup.AddSecurityRequirement( SwaggerConfiguration.SecurityRequirements );
} );

builder.Services
       .AddHttpContextAccessor()
       .AddSingleton<ITokenLifetimeManager, JwtTokenLifetimeManager>()
       .AddSingleton<ITokenReplayManager, JwtTokenReplayManager>()
       .AddSingleton<IAccountAuthentication, StupidCredentialsCheck>();

builder.Services
       .AddAuthentication( AuthenticationConfiguration.SetUpOptions )
       .AddJwtBearer( JwtBearerConfiguration.SetUp( builder ) );

builder.Services.AddAuthorization();

builder.Services.Configure<JwtSettings>( builder.Configuration.GetSection( JwtSettings.SectionName ) );

// Configure MVC Components
builder.Services.AddControllersWithViews(); // TODO: Minimal APIs instead?

WebApplication app = builder.Build(); // ----------------------------------------------------------

app.UseJwtTokenManagement();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
   app.UseSwagger( options => options.SerializeAsV2 = true );
   app.UseSwaggerUI();
}
else
{
   // The default HSTS value is 30 days. You may want to change this for
   // production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.MapGet( "/api/hello", [Authorize]() => "Hello!" );

app.MapControllerRoute( "default", "/api/{controller}/{action=Index}/{id?}" );
app.MapFallbackToFile( "index.html" );

app.Run();
