using System.Security.Claims;

namespace NetIdentityModel.Models.AccountDto
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Cria Role", "Cria Role"),
            new Claim("Edita Role","Edita Role"),
            new Claim("Deleta Role","Deleta Role")
        };
    }
}
