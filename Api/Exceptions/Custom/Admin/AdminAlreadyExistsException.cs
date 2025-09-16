using MinimalApi.Exceptions.Base;

namespace MinimalApi.Exceptions.Custom.Admin;

public class AdminAlreadyExistsException : BaseAlreadyExistsException
{
    public AdminAlreadyExistsException() : base("Administrador ja existe") { }  
    public AdminAlreadyExistsException(string message) : base(message) { }
    public AdminAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
}