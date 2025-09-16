using MinimalApi.Exceptions.Base;

namespace MinimalApi.Exceptions.Custom.Mapping;

public class UnableToMapException : BaseNotImplementedException
{
    public UnableToMapException() : base("Unable to map") { }
    public UnableToMapException(string message) : base(message) { }
    public UnableToMapException(string message, Exception innerException) : base(message, innerException) { }
    
}