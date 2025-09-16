using MinimalApi.Exceptions.Base;

namespace MinimalApi.Exceptions.Custom.Veiculo;

public class VeiculoAlreadyExistsException : BaseAlreadyExistsException
{
    
    public VeiculoAlreadyExistsException() : base("Veiculo já cadastrado") { }
    public VeiculoAlreadyExistsException(string message) : base(message) { }

    public VeiculoAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
}