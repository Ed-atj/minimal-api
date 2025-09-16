using MinimalApi.Domain.Entities;

namespace MinimalApi.Interfaces.Admin;

public interface IAdministradorRepository
{
    Administrador? BuscarPorId(int id);
    
    Administrador? BuscarPorEmail(string email);
    bool ExistePorId(int id);
    
    bool ExistePorEmail(string email);
    
    Administrador Salvar(Administrador administrador);
    
    Administrador Alterar(Administrador administrador);
    
    List<Administrador> Listar();
    
    void Excluir(Administrador administrador);
}