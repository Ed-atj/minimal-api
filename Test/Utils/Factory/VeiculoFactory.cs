using MinimalApi.Domain.Entities;

namespace Test.Utils.Factory;

public class VeiculoFactory
{
    private readonly Veiculo _veiculo = new()
    {
        Id = 10,
        Nome = "Uno",
        Marca = "Fiat",
        Ano = 2022,
    };

    public Veiculo Criar()
    {
        return _veiculo;
    }

    public VeiculoFactory ComId(int id)
    {
        _veiculo.Id = id;
        return this;
    }

    public VeiculoFactory ComNome(string nome)
    {
        _veiculo.Nome = nome;
        return this;
    }
    public VeiculoFactory ComMarca(string marca)
    {
        _veiculo.Marca = marca;
        return this;
    }
    public VeiculoFactory ComAno(int ano)
    {
        _veiculo.Ano = ano;
        return this;
    }
}