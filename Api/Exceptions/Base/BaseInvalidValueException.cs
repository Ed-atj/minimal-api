namespace MinimalApi.Exceptions.Base;

public class BaseInvalidValueException : Exception
{
    public BaseInvalidValueException(string message) : base(message) { }
    public BaseInvalidValueException(string message, Exception innerException) : base(message, innerException){}
}