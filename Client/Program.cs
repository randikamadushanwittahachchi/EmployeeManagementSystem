using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Client;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implementations;
using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;
using ClientLibrary.Authentication;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Blazored.Modal;
using Client.ApplicationState;
using Client.Helper.Constracts;
using Client.Helper.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();

builder.Services.AddHttpClient("SystemApiClinet", Client => { Client.BaseAddress = new Uri("https://localhost:7293"); }).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7293") });

//Add Herlper to Service
builder.Services.AddTransient<ISerialization, Serialization>();
builder.Services.AddScoped<ILocalStorage, LocalStorage>();
builder.Services.AddScoped<IGetHttpClient, GetHttpClient>();
//Authuthorization service add
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//
builder.Services.AddScoped<IUserAccountService, UserAccounmService>();

builder.Services.AddScoped<DepartmentState>();

builder.Services.AddBlazoredModal();
builder.Services.AddScoped<IModalDialog, ModalDialog>();
builder.Services.AddScoped<IModalInputDialog, ModalInputDialog>();

await builder.Build().RunAsync();
