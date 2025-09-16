using MinimalApi.Domain.Entities;

namespace Test.Utils.Factory;


public class AdministradorFactory
{
    
    private readonly Administrador _administrador = new()
    {
        Id = 10,
        Email = "teste@example.com",
        Senha = "123456",
        Perfil = "Adm"
    };

    public Administrador Criar()
    {
        return _administrador;
    }
    
    public AdministradorFactory ComId(int id)
    {
        _administrador.Id = id;
        return this;
    }

    public AdministradorFactory ComEmail(string email)
    {
        _administrador.Email = email;
        return this;
    }
    public AdministradorFactory ComSenha(string senha)
    {
        _administrador.Senha = senha;
        return this;
    }
    public AdministradorFactory ComPerfil(string perfil)
    {
        _administrador.Perfil = perfil;
        return this;
    }
}