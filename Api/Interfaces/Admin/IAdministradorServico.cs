using MinimalApi.Domain.Dto.Request.login;
using MinimalApi.Domain.Dto.Response.admin;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Interfaces.Admin;

public interface IAdministradorServico
{
    AdministradorLogado Login(LoginDto loginDto);
    Administrador BuscaPorId(int id);
    
    Administrador Salvar(Administrador administrador);
    
    Administrador? Alterar(Administrador administrador);
    
    List<Administrador>? Listar();
    
    void Excluir(int id);
}