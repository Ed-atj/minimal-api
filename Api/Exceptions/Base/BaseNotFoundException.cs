namespace MinimalApi.Exceptions.Base;

public class BaseNotFoundException : Exception
{
    public BaseNotFoundException(string message) : base(message) { }
    public BaseNotFoundException(string message, Exception innerException) : base(message, innerException){}
}