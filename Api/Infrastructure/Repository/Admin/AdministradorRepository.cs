using Lombok.NET;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
using MinimalApi.Db;
using MinimalApi.Interfaces.Admin;

namespace MinimalApi.Infrastructure.Repository.Admin;

[RequiredArgsConstructor]
public partial class AdministradorRepository : IAdministradorRepository
{
    private readonly DbContexto _contexto;


    public Administrador? BuscarPorId(int id)
    {
        return _contexto.Administradores.FirstOrDefault(v => v.Id == id);
    }

    public Administrador? BuscarPorEmail(string email)
    {
        return _contexto.Administradores.FirstOrDefault(v => v.Email == email);
    }

    public bool ExistePorId(int id)
    {
        return _contexto.Administradores.Any(v => v.Id == id);
    }

    public bool ExistePorEmail(string email)
    {
        return _contexto.Administradores.Any(v => v.Email == email);
    }

    public Administrador Salvar(Administrador administrador)
    {
        _contexto.Administradores.Add(administrador);
        _contexto.SaveChanges();
        return administrador;
    }

    public Administrador Alterar(Administrador administrador)
    {
        _contexto.Administradores.Update(administrador);
        _contexto.SaveChanges();
        return administrador;
    }

    public List<Administrador> Listar()
    {
        List<Administrador> listAdmin = _contexto.Administradores.ToList();
        return listAdmin;
    }

    public void Excluir(Administrador administrador)
    {
        _contexto.Administradores.Remove(administrador);
        _contexto.SaveChanges();
    }

    public async Task<Administrador?> BuscarPorIdAsync(int id)
    {
        return await _contexto.Administradores.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task SalvarAsync(Administrador administrador)
    {
        _contexto.Administradores.Add(administrador);
        await _contexto.SaveChangesAsync();
    }
}