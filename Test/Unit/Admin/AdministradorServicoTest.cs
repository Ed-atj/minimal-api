using Microsoft.Extensions.Logging;
using MinimalApi.Domain.Dto.Request.login;
using MinimalApi.Interfaces.Admin;
using MinimalApi.Interfaces.Password;
using MinimalApi.Interfaces.Token;
using MinimalApi.Services.Admin;
using MinimalApi.Utils;
using Moq;
using Test.Utils.Factory;
using Xunit;
using Assert = Xunit.Assert;

namespace Test.Unit.Admin;

public class AdministradorServicoTest
{
    private readonly Mock<IAdministradorRepository> _mockRepository;
    private readonly Mock<IHashPassword> _mockHashPassword;
    private readonly Mock<IToken> _mockToken;
    private readonly IAdministradorServico _administradorServico;
    
    private readonly AdministradorFactory _administradorFactory = new();

    public AdministradorServicoTest()
    {
        _mockRepository = new Mock<IAdministradorRepository>();
        _mockHashPassword = new Mock<IHashPassword>();
        _mockToken = new Mock<IToken>();
        var logger = new Mock<ILogger<AdministradorServico>>();
        var dataUtils = new DataUtils();
        _administradorServico = new AdministradorServico(_mockRepository.Object
            , _mockHashPassword.Object, _mockToken.Object, logger.Object, dataUtils);
    }

    [Fact]
    public void Login_DeveRetornarAdministradorLogado()
    {
        // Arrange
        var admin = _administradorFactory.Criar();
        var loginDto = new LoginDto()
        {
            Email = admin.Email,
            Senha = admin.Senha
        };
        
        _mockRepository.Setup(r => r.BuscarPorEmail(loginDto.Email)).Returns(admin);
        _mockHashPassword.Setup(h => h.Verify(loginDto.Senha, admin.Senha)).Returns(true);
        _mockToken.Setup(t => t.GerarTokenJwt(admin)).Returns("token_gerado");

        // Act
        var result = _administradorServico.Login(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(admin.Email, result.Email);
        Assert.Equal("token_gerado", result.Token);
    }
}