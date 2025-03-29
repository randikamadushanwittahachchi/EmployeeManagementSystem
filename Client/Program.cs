using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//Add Herlper to Service
builder.Services.AddTransient<ISerialization, Serialization>();
builder.Services.AddTransient<ILocalStorage, LocalStorage>();
builder.Services.AddScoped<IGetHttpClient, GetHttpClient>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
