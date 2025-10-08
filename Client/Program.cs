using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Blazored.LocalStorage;
using Blazored.Modal;
using Client;
using Client.Helper.Constracts;
using Client.Helper.Implementations;
using Client.Helper.Implementations.AdministrationModal;
using Client.Helper.Implementations.DoctorModal;
using Client.Helper.Implementations.OverTime;
using Client.Helper.Implementations.OverTimeModal;
using Client.Helper.Implementations.ProfileModal;
using Client.Helper.Implementations.SanctionModal;
using Client.Helper.Implementations.VacationModals;
using Client.State;
using ClientLibrary.Authentication;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implementations;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.IdentityModel.Tokens;

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

// Doctor / Vacation / VacationType / OverTime / OverTimeType / Saction / SanctionType
builder.Services.AddScoped<IGenericServiceInterface<Doctor>, GenericServic<Doctor>>();
builder.Services.AddScoped<IGenericServiceInterface<DoctorType>, GenericServic<DoctorType>>();
builder.Services.AddScoped<IGenericServiceInterface<Vacation>, GenericServic<Vacation>>();
builder.Services.AddScoped<IGenericServiceInterface<VacationType>, GenericServic<VacationType>>();
builder.Services.AddScoped<IGenericServiceInterface<OverTime>, GenericServic<OverTime>>();
builder.Services.AddScoped<IGenericServiceInterface<OverTimeType>, GenericServic<OverTimeType>>();
builder.Services.AddScoped<IGenericServiceInterface<Sanction>, GenericServic<Sanction>>();
builder.Services.AddScoped<IGenericServiceInterface<SanctionType>, GenericServic<SanctionType>>();



builder.Services.AddScoped<AllState>();

builder.Services.AddBlazoredModal();
builder.Services.AddScoped<IDialogModal, DialogModal>();
builder.Services.AddScoped<IGenericModal<GeneralDepartment>, GeneralDeparmentModal>();
builder.Services.AddScoped<IGenericModal<Department>, DepartmentModal>();
builder.Services.AddScoped<IGenericModal<Branch>, BranchModal>();
builder.Services.AddScoped<IGenericModal<Country>, CountryModal>();
builder.Services.AddScoped<IGenericModal<City>, CityModal>();
builder.Services.AddScoped<IGenericModal<Town>, TownModal>();
builder.Services.AddScoped<EmployeeModal>();
builder.Services.AddScoped<AdministrationModal>();
builder.Services.AddScoped<MenuModal>();
builder.Services.AddScoped<ViewModal>();
builder.Services.AddScoped<DoctorModal>();
builder.Services.AddScoped<DoctorTypeModal>();
builder.Services.AddScoped<OverTimeModal>();
builder.Services.AddScoped<OverTimeTypeModal>();
builder.Services.AddScoped<SanctionModal>();
builder.Services.AddScoped<SanctionTypeModal>();
builder.Services.AddScoped<VacationModal>();
builder.Services.AddScoped<VacationTypeModal>();
builder.Services.AddScoped<ProfileEditModal>();
builder.Services.AddScoped<ProfileModal>();

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
