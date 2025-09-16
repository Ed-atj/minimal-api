namespace MinimalApi.Exceptions.Base;

public class BaseNotImplementedException : NotImplementedException
{
    public BaseNotImplementedException() : base("Not implemented") { }
    public BaseNotImplementedException(string message) : base(message) { }
    public BaseNotImplementedException(string message, Exception innerException) : base(message, innerException) { }
}