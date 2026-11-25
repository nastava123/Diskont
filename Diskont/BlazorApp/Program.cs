using BlazorApp;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// adresa Web API-ja čita se iz wwwroot/appsettings.json
string apiUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001/";

// HttpClient šalje sve zahteve na Web API, a ne na adresu same Blazor aplikacije
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

// klijentski servisi: jedino mesto koje zna adrese /api/...
builder.Services.AddScoped<CategoryApi>();
builder.Services.AddScoped<ProductApi>();
builder.Services.AddScoped<UserApi>();
await builder.Build().RunAsync();
