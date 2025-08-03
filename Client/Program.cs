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
using Client.State;
using Client.Helper.Constracts;
using Client.Helper.Implementations;
using BaseLibrary.Entities;
using BaseLibrary.DTOs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();

builder.Services.AddHttpClient("SystemApiClinet", Client => { Client.BaseAddress = new Uri("https://localhost:7293"); }).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7293") });

builder.Services.AddScoped<IGenericServiceInterface<GeneralDepartment>, GenericServic<GeneralDepartment>>();
builder.Services.AddScoped<IGenericServiceInterface<Department>, GenericServic<Department>>();
builder.Services.AddScoped<IGenericServiceInterface<Branch>, GenericServic<Branch>>();
builder.Services.AddScoped<IGenericServiceInterface<Country>, GenericServic<Country>>();
builder.Services.AddScoped<IGenericServiceInterface<City>, GenericServic<City>>();
builder.Services.AddScoped<IGenericServiceInterface<Town>, GenericServic<Town>>();
builder.Services.AddScoped<IGenericServiceInterface<ManageUser>, GenericServic<ManageUser>>();
builder.Services.AddScoped<IGenericServiceInterface<Employee>, GenericServic<Employee>>();


builder.Services.AddScoped<AllState>();

builder.Services.AddBlazoredModal();
builder.Services.AddScoped<ManageUserModal>();
builder.Services.AddScoped<IDialogModal, DialogModal>();
builder.Services.AddScoped<IGenericModal<GeneralDepartment>, GeneralDeparmentModal>();
builder.Services.AddScoped<IGenericModal<Department>, DepartmentModal>();
builder.Services.AddScoped<IGenericModal<Branch>, BranchModal>();
builder.Services.AddScoped<IGenericModal<Country>, CountryModal>();
builder.Services.AddScoped<IGenericModal<City>, CityModal>();
builder.Services.AddScoped<IGenericModal<Town>, TownModal>();
builder.Services.AddScoped<EmployeeModal>();
builder.Services.AddScoped<MenuModal>();
builder.Services.AddScoped<ViewModal>();

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


await builder.Build().RunAsync();
