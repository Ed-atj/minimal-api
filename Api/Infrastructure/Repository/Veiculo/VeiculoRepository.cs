using Lombok.NET;
using MinimalApi.Db;
using MinimalApi.Interfaces.Veiculo;
using VeiculoEntity = MinimalApi.Domain.Entities.Veiculo;

namespace MinimalApi.Infrastructure.Repository.Veiculo;

[RequiredArgsConstructor]
public partial class VeiculoRepository : IVeiculoRepository
{
    private readonly DbContexto _contexto;

    public void Apagar(VeiculoEntity veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();
    }

    public void Atualizar(VeiculoEntity veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();
    }

    public List<VeiculoEntity> Listar()
    {
        return _contexto.Veiculos.ToList();
    }

    public VeiculoEntity Salvar(VeiculoEntity veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();
        return veiculo;
    }


    public VeiculoEntity? BuscarPorId(int id)
    {
        return _contexto.Veiculos.FirstOrDefault(v => v.Id == id);
    }

    public bool ExistePorId(int id)
    {
        return _contexto.Veiculos.Any(v => v.Id == id);
    }
}