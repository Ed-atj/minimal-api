using MinimalApi.Domain.Entities;

namespace MinimalApi.Interfaces.Token;

public interface IToken
{
    string GerarTokenJwt(Administrador administrador);
}