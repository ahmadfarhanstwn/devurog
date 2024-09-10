using IdentityService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using IdentityService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdentityService.Helpers;
using IdentityService.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthSettings.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDBContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityServiceDB"));
});
builder.Services.AddTransient<JWTTokenService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => 
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service API", Version = "v1" });
});
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<JWTTokenService>();

var app = builder.Build();

app.UseAuthentication(); // This enables authentication middleware
app.UseAuthorization();  // This enables authorization middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service API V1");
    });
}

app.Run();
