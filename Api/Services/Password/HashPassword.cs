using Lombok.NET;
using MinimalApi.Exceptions.Custom.Password;
using MinimalApi.Interfaces.Password;

namespace MinimalApi.Services.Password;

[RequiredArgsConstructor]
public partial class HashPassword : IHashPassword
{
    private readonly ILogger<HashPassword> _logger;
    public string Hash(string password)
    {
        if (!string.IsNullOrEmpty(password)) return BCrypt.Net.BCrypt.HashPassword(password);
        _logger.LogWarning("Senha invalida");
        throw new PasswordInvalidHashException("Senha invalida");
    }
    
    public bool Verify(string password, string hashedPassword)
    {
        if (string.IsNullOrEmpty(password)) return false;
        return !string.IsNullOrEmpty(hashedPassword) 
               && BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}