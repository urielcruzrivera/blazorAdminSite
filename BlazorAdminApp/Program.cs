using BlazorAdminApp;
using BlazorAdminApp.Helpers;
using BlazorAdminApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
           .AddScoped<IAuthenticationService, AuthenticationService>()
           .AddScoped<IUserService, UserService>()
           .AddScoped<IHttpService, HttpService>()
           .AddScoped<ILocalStorageService, LocalStorageService>();

// configure http client
builder.Services.AddScoped(x =>
{
    //var apiUrl = new Uri(builder.Configuration["AppServices:BaseAdress"]);
    var apiUrl = new Uri(builder.Configuration["apiUrl"]);

    // use fake backend if "fakeBackend" is "true" in appsettings.json
    if (builder.Configuration["fakeBackend"] == "true")
        return new HttpClient(new FakeBackendHandler()) { BaseAddress = apiUrl };

    return new HttpClient() { BaseAddress = apiUrl };
});

//await builder.Build().RunAsync();
//var authConf = new AppServices();
//builder.Configuration.GetSection(nameof(AppServices)).Bind(authConf);

//builder.Services.Configure<AppServices>(builder.Configuration.GetSection("AppServices"));

var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.Initialize();

await host.RunAsync();
