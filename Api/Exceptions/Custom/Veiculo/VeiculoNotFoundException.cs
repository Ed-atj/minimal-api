using MinimalApi.Exceptions.Base;

namespace MinimalApi.Exceptions.Custom.Veiculo;

public class VeiculoNotFoundException : BaseNotFoundException
{
    public VeiculoNotFoundException() : base("Veiculo nao encontrado") { }
    public VeiculoNotFoundException(string message) : base(message) { }
    public VeiculoNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}