using System.ComponentModel.DataAnnotations;
using Lombok.NET;
using MinimalApi.Domain.Dto.Request.login;
using MinimalApi.Domain.Dto.Response.admin;
using MinimalApi.Domain.Entities;
using MinimalApi.Exceptions.Custom.Admin;
using MinimalApi.Exceptions.Custom.Password;
using MinimalApi.Interfaces.Admin;
using MinimalApi.Interfaces.Password;
using MinimalApi.Interfaces.Token;
using MinimalApi.Utils;

namespace MinimalApi.Services.Admin;

[RequiredArgsConstructor]
public partial class AdministradorServico : IAdministradorServico
{
    
    private readonly IAdministradorRepository _administradorRepository;
    private readonly IHashPassword _hashPassword;
    private readonly IToken _token;
    private readonly ILogger<AdministradorServico> _logger;
    private readonly DataUtils _dataUtils;
    
    public AdministradorLogado Login(LoginDto loginDto)
    {
        var admin = _administradorRepository.BuscarPorEmail(loginDto.Email);
        if(admin == null)
        {
            _logger.LogWarning("Administrador não encontrado para o email: {Email}", _dataUtils.MascaraEmail(loginDto.Email));
            throw new AdminNotFoundException();
        }
        if (!_hashPassword.Verify(loginDto.Senha, admin.Senha))
        {
            _logger.LogWarning("Senha invalida para o email: {Email}", _dataUtils.MascaraEmail(loginDto.Email));
            throw new PasswordInvalidHashException("Senha invalida");
        }
        
        var token = _token.GerarTokenJwt(admin);
        return new AdministradorLogado(admin.Email, admin.Perfil, token);
    }

    public Administrador BuscaPorId(int id)
    {
        var admin = _administradorRepository.BuscarPorId(id);
        if (admin != null) return admin;
        _logger.LogWarning("Administrador não encontrado para o id: {Id}", _dataUtils.MascaraId(id));
        throw new AdminNotFoundException();

    }
    

    public Administrador Salvar(Administrador administrador)
    {
        if (_administradorRepository.ExistePorEmail(administrador.Email))
        {
            _logger.LogWarning("Administrador ja existe para o Email: {Email}", _dataUtils.MascaraEmail(administrador.Email));
            throw new AdminAlreadyExistsException();    
        }
        administrador.Senha = _hashPassword.Hash(administrador.Senha);
        var storedAdmin = _administradorRepository.Salvar(administrador);
        return storedAdmin ?? throw new InvalidOperationException();
    }

    public Administrador? Alterar(Administrador administrador)
    {
        var admin = BuscaPorId(administrador.Id);
        return _administradorRepository.Alterar(admin);
    }

    public List<Administrador> Listar()
    {
        var listAdmin = _administradorRepository.Listar();
        if (listAdmin == null || listAdmin.Count == 0)
        {
            _logger.LogWarning("Lista de administradores vazia ou nula");
            throw new AdminNotFoundException();
        }
        return listAdmin;
    }

    public void Excluir(int id)
    {
        var adminToDelete = _administradorRepository.BuscarPorId(id);
        if (adminToDelete == null)
        {
            _logger.LogWarning("Administrador não encontrado para o id: {Id}", _dataUtils.MascaraId(id));
            throw new AdminNotFoundException();
        }
        _administradorRepository.Excluir(adminToDelete);
    }
}