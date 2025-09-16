using VeiculoEntity = MinimalApi.Domain.Entities.Veiculo;

namespace MinimalApi.Interfaces.Veiculo;

public interface IVeiculoServico
{
    List<VeiculoEntity> Listar();
    VeiculoEntity BuscarPorId(int id);
    VeiculoEntity? Salvar(VeiculoEntity veiculo);
    void Atualizar(int id, VeiculoEntity veiculo);
    void Apagar(int id);
}