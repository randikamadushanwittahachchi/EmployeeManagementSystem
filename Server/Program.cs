using Microsoft.EntityFrameworkCore;
using ServerLibrary.Authentication;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;
using ServerLibrary.Repositores.Implementations;
using ServerLibrary.Services.Contracts;
using ServerLibrary.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Swagger services correctly
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Register
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Sorry, Your Connection is not found"));
});

builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//Configer
builder.Services.Configure<JWTSection>(builder.Configuration.GetSection("JWTSection"));
//Service 
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IUserAccountService,UserAccountService>();
builder.Services.AddScoped<ISystemRoleService,SystemRoleService>();
builder.Services.AddScoped<IUserRoleService,UserRoleService>();

//Authentication
builder.Services.AddScoped<TokenService>();

//Repositores
builder.Services.AddScoped<IUserAccount, UserAccountRepositore>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
