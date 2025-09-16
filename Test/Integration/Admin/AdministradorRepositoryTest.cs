using MinimalApi.Db;
using MinimalApi.Infrastructure.Repository.Admin;
using Test.Integration.Connection;
using Test.Utils.Factory;
using Xunit;
using Assert = Xunit.Assert;

namespace Test.Integration.Admin;

public class AdministradorRepositoryTest : IClassFixture<DatabaseConnection>, IAsyncLifetime
{

    private readonly DbContexto _context;
    private readonly AdministradorRepository _administradorRepository;
    private readonly AdministradorFactory _administradorFactory;

    public AdministradorRepositoryTest(DatabaseConnection databaseConnection)
    {
        _context = databaseConnection.Context;   
        _administradorRepository = new AdministradorRepository(databaseConnection.Context);
        _administradorFactory = new AdministradorFactory();
    }

    public async Task InitializeAsync()
    {
        _context.Administradores.RemoveRange(_context.Administradores);
        await _context.SaveChangesAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task Salvar_DeveIncluirAdministrador()
    {
        var administrador = _administradorFactory.Criar();
        
        await _administradorRepository.SalvarAsync(administrador);
        Assert.True(administrador.Id > 0);
        
        var administradorSalvo = await _administradorRepository.BuscarPorIdAsync(administrador.Id);
        Assert.NotNull(administradorSalvo);
        Assert.Equal(administrador.Email, administradorSalvo.Email);
    }

    [Fact]
    public void BuscarPorId_DeveRetornarAdministrador()
    {
        
        var administrador = _administradorFactory.Criar();
        _administradorRepository.Salvar(administrador);
        
        var result = _administradorRepository.BuscarPorId(administrador.Id);
        Assert.NotNull(result);
        Assert.Equal(administrador.Email, result.Email);
    }
    
    
    
}