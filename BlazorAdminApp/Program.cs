using BlazorAdminApp;
using BlazorAdminApp.Helpers;
using BlazorAdminApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var appSettings = builder.Configuration.GetSection("AppServices").Get<AppServices>();
builder.Services.AddSingleton(appSettings);

builder.Services
           .AddScoped<IAuthenticationService, AuthenticationService>()
           .AddScoped<IHttpService, HttpService>()
           .AddScoped<ILocalStorageService, LocalStorageService>()
           .AddScoped<IBitacoraService, BitacoraService>()
           .AddScoped<ICorteCajaService, CorteCajaService>()
           .AddScoped<IUsuarioService, UsuarioService>()
           .AddScoped<ICajaService, CajaService>();

builder.Services.AddScoped(x =>
{
    var apiUrl = new Uri(builder.Configuration["AppServices:BaseAdress"]);    
    return new HttpClient() { BaseAddress = apiUrl };
});

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();

var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.Initialize();

await host.RunAsync();
