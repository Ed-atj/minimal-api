using MinimalApi.Domain.Dto.Request.veiculo;
using MinimalApi.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace MinimalApi.mappers;

[Mapper]
public partial  class VeiculoMapper
{
    [MapperIgnoreTarget(nameof(Veiculo.Id))]
    public partial Veiculo ToVeiculo(VeiculoDto veiculoDto);

    [MapperIgnoreSource(nameof(Veiculo.Id))]
    public partial VeiculoDto ToVeiculoDto(Veiculo veiculo);
}
