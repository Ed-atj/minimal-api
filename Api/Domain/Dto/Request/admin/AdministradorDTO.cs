
using MinimalApi.Domain.Enums;

namespace MinimalApi.Domain.Dto.Request.admin;

public record AdministradorDto
{
    public string Email { get;set; } = default!;
    public string Senha { get;set; } = default!;
    public Perfil Perfil { get;set; } = default!;
    
    
    
    
    
}