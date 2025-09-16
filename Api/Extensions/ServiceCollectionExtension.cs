using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApi.Db;
using MinimalApi.Infrastructure.Repository.Admin;
using MinimalApi.Infrastructure.Repository.Veiculo;
using MinimalApi.Interfaces.Admin;
using MinimalApi.Interfaces.Password;
using MinimalApi.Interfaces.Token;
using MinimalApi.Interfaces.Veiculo;
using MinimalApi.Services.Password;
using AdministradorServico = MinimalApi.Services.Admin.AdministradorServico;
using TokenService = MinimalApi.Services.Token.TokenService;
using VeiculoServico = MinimalApi.Services.Veiculo.VeiculoServico;

namespace MinimalApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do JWT
        var jwtKey = configuration.GetSection("Jwt:Key").Value ?? throw new InvalidOperationException("JWT Key not found.");
        

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };
        });
        services.AddAuthorization();

        // Injeção de Dependência
        // Hash e Token
        services.AddSingleton<IHashPassword, HashPassword>();
        services.AddSingleton<IToken, TokenService>();
        
        //Serviços
        services.AddScoped<IAdministradorServico, AdministradorServico>();
        services.AddScoped<IVeiculoServico, VeiculoServico>();
        
        //Repositorios
        services.AddScoped<IAdministradorRepository, AdministradorRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();

        // Configuração do DbContext
        services.AddDbContext<DbContexto>(options =>
        {
            options.UseMySql(
                configuration.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("MySql"))
            );
        });

        // Configuração do Swagger/OpenAPI
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT aqui"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                    Array.Empty<string>()
                }
            });
        });

        // Configuração do CORS
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                corsBuilder =>
                {
                    corsBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }
}