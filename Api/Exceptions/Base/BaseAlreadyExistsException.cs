namespace MinimalApi.Exceptions.Base;

public class BaseAlreadyExistsException : Exception
{
    public BaseAlreadyExistsException(string message) : base(message) { }
    public BaseAlreadyExistsException(string message, Exception innerException) : base(message, innerException){}

}