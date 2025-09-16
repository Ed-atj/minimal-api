namespace MinimalApi.Interfaces.Password;

public interface IHashPassword
{
    string Hash(string password);
    
    bool Verify(string password, string hashedPassword);

}