using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lombok.NET;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Domain.Entities;
using MinimalApi.Interfaces.Token;

namespace MinimalApi.Services.Token;

[RequiredArgsConstructor]
public partial class TokenService : IToken
{

    private readonly string _key;
    private readonly ILogger<TokenService> _logger;

    public string GerarTokenJwt(Administrador administrador){
        if(string.IsNullOrEmpty(_key)) return string.Empty;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>()
        {
            new("Email", administrador.Email),
            new("Perfil", administrador.Perfil),
            new(ClaimTypes.Role, administrador.Perfil),
        };
        
        if(claims.Count == 0){
            _logger.LogWarning("Nenhuma claim foi adicionada ao token");
            throw new InvalidOperationException("Nenhuma claim foi adicionada ao token");
        }
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        _logger.LogInformation("Token gerado para o email: {Email}", administrador.Email);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}