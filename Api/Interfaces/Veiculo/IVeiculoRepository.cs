using VeiculoEntity = MinimalApi.Domain.Entities.Veiculo;

namespace MinimalApi.Interfaces.Veiculo;

public interface IVeiculoRepository
{
    void Apagar(VeiculoEntity veiculo);
    void Atualizar(VeiculoEntity veiculo);
    List<VeiculoEntity> Listar();
    
    VeiculoEntity Salvar(VeiculoEntity veiculo);
    
    VeiculoEntity? BuscarPorId(int id);
    
    bool ExistePorId(int id);
    
}