using MinimalApi.Domain.Dto.Request.login;

namespace MinimalApi.Utils;

public class DataUtils
{

    public string MascaraEmail(string email)
    {
        string[] partesEmail = email.Split('@');
        string emailMascarado = partesEmail[0].Substring(0, 2) + "***@" + partesEmail[1];
        return emailMascarado;
    }

    public string MascaraId(int id)
    {
        string idString = id.ToString();
        if (idString.Length <= 4)
        {
            return $"ID-{idString[0]}***";
        }        
        string prefixo = idString.Substring(0, 3);
        string sufixo = idString.Substring(idString.Length - 3);
        return $"ID-{prefixo}***{sufixo}";
    }
    
}