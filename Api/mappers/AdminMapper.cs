using MinimalApi.Domain.Dto.Request.admin;
using MinimalApi.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace MinimalApi.mappers;

[Mapper]
public partial class AdminMapper
{

    [MapperIgnoreTarget(nameof(Administrador.Id))]
    
    public partial Administrador ToAdministrador(AdministradorDto administradorDto);

    [MapperIgnoreSource(nameof(Administrador.Id))]
    public partial AdministradorDto ToAdministradorDto(Administrador administrador);
}