using Microsoft.AspNetCore.Identity;

namespace NetIdentityModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        //propriedades especializadas extendidas
        public string? UserExtended { get; set; }
        public string? Cpf { get; set; }
    }
}
