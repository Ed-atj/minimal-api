
namespace MinimalApi.Domain.Dto.Request.veiculo;
public record VeiculoDto
{
    public string Nome { get;set; } = default!;
    public string Marca { get;set; } = default!;
    public int Ano { get;set; } = default!;
}