using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Authentication;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;
using ServerLibrary.Repositores.Implementations;
using ServerLibrary.Services.Contracts;
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
//Service 
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IUserAccountService,UserAccountService>();
builder.Services.AddScoped<ISystemRoleService,SystemRoleService>();
builder.Services.AddScoped<IUserRoleService,UserRoleService>();
builder.Services.AddScoped<IRefreshTokenInfoService , RefreshTokenInfoService>();

//Authentication
builder.Services.AddScoped<TokenService>();

//Repositores
builder.Services.AddScoped<IUserAccount, UserAccountRepositore>();

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
