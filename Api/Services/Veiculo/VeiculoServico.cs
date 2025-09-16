using Lombok.NET;
using MinimalApi.Exceptions.Custom.Veiculo;
using MinimalApi.Interfaces.Veiculo;
using MinimalApi.Utils;
using VeiculoEntity = MinimalApi.Domain.Entities.Veiculo;

namespace MinimalApi.Services.Veiculo;

[RequiredArgsConstructor]
public partial class VeiculoServico : IVeiculoServico
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ILogger<VeiculoServico> _logger;
    private readonly DataUtils _dataUtils;
    
    public void Apagar(int id)
    {
        var veiculo = _veiculoRepository.BuscarPorId(id);
        if (veiculo == null)
        {
            _logger.LogWarning("Veiculo não encontrado para o id: {Id}", _dataUtils.MascaraId(id));
            throw new VeiculoNotFoundException("Veiculo não encontrado");
        }
        _veiculoRepository.Apagar(veiculo);  
    }


    public void Atualizar(int id, VeiculoEntity veiculo)
    {
        var veiculoToUpdate = _veiculoRepository.BuscarPorId(id);
        if (veiculoToUpdate == null)
        {
            _logger.LogWarning("Veiculo não encontrado para o id: {Id}", _dataUtils.MascaraId(id));
            throw new VeiculoNotFoundException("Veiculo não encontrado");
        }
        veiculoToUpdate.Nome = veiculo.Nome;
        veiculoToUpdate.Marca = veiculo.Marca;
        veiculoToUpdate.Ano = veiculo.Ano;

        _veiculoRepository.Atualizar(veiculoToUpdate);
    }

    public VeiculoEntity BuscarPorId(int id)
    {
        var veiculoFound = _veiculoRepository.BuscarPorId(id);
        return veiculoFound ?? throw new VeiculoNotFoundException();
    }

    public VeiculoEntity? Salvar(VeiculoEntity veiculo)
    {
        if (!_veiculoRepository.ExistePorId(veiculo.Id)) return _veiculoRepository.Salvar(veiculo);
        _logger.LogWarning("Veiculo já cadastrado para o id: {Id}", _dataUtils.MascaraId(veiculo.Id));    
        throw new VeiculoAlreadyExistsException();
    }

    public List<VeiculoEntity> Listar()
    {
        var listVeiculo = _veiculoRepository.Listar();
        if (listVeiculo.Count != 0) return listVeiculo;
        _logger.LogWarning("Lista de veículos vazia");    
        throw new VeiculoNotFoundException("Lista de veículos vazia");
    }
}