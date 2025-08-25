using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Authentication;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;
using ServerLibrary.Repositores.Implementations;
using ServerLibrary.Services.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Swagger services correctly
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSection = builder.Configuration.GetSection(nameof(JWTSection)).Get<JWTSection>();

//Database Register
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Sorry, Your Connection is not found"));
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection!.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection!.Key!))
    };

});

builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//Configer
builder.Services.Configure<JWTSection>(builder.Configuration.GetSection("JWTSection"));


// Repositories 
        // Country / City / Twon
builder.Services.AddScoped<IGenericRepositoryInterface<Country>, CountryRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<City>, CityRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Town>, TownRepository>();

        // GeneralDepartment / Department / Branch
builder.Services.AddScoped<IGenericRepositoryInterface<GeneralDepartment>, GeneralDepartmentRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Department>, DepartmentRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Branch>, BranchRepository>();

        // Employee
builder.Services.AddScoped<IGenericRepositoryInterface<Employee>, EmployeeRepository>();

        // SystemRole / UseraRole / RefreshTokenInfo
builder.Services.AddScoped<SystemRoleRepository>();
builder.Services.AddScoped<UserRoleRepository>();
builder.Services.AddScoped<RefreshTokenInfoRepository>();

        // Doctor / OverTime / OverTimeType / Vacation / VacationType / Sanction / SanctionType
builder.Services.AddScoped<IGenericRepositoryInterface<Doctor>, DoctorRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<OverTime>, OverTimeRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<OverTimeType>, OverTimeTypeRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Sanction>, SanctionRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<SanctionType>, SanctionTypeRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Vacation>, VacationRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<VacationType>, VacationTypeRepository>();

        


//Authentication
builder.Services.AddScoped<TokenService>();

//Repositores
builder.Services.AddScoped<UserAccountRepositore>();

//Service 
builder.Services.AddScoped<ManageUserService>();
// 
builder.Services.AddCors(option => {
    option.AddPolicy("AllowBlazorWasm", builder => builder.WithOrigins("http://localhost:5089","https://localhost:7230")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}


// Enable Swagger in Development Mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
