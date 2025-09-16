using MinimalApi.Exceptions.Base;

namespace MinimalApi.Exceptions.Custom.Admin;

public class AdminNotFoundException : BaseNotFoundException
{
    public AdminNotFoundException() : base("Administrador nao encontrado") { }
    public AdminNotFoundException(string message) : base(message) { }
    public AdminNotFoundException(string message, Exception innerException) : base(message, innerException) { }

}