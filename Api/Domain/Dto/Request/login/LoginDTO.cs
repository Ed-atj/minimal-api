
namespace MinimalApi.Domain.Dto.Request.login;
public record LoginDto
{
    public string Email { get;set; } = default!;
    public string Senha { get;set; } = default!;
}