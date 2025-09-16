using MinimalApi.Exceptions.Base;

namespace MinimalApi.Exceptions.Custom.Password;

public class PasswordInvalidHashException : BaseInvalidValueException
{
    public PasswordInvalidHashException(string message) : base(message)
    {
    }

    public PasswordInvalidHashException(string message, Exception innerException) : base(message, innerException)
    {
    }
}