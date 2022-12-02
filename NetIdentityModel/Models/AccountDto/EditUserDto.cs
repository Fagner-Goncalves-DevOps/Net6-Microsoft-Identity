using System.ComponentModel.DataAnnotations;

namespace NetIdentityModel.Models.AccountDto
{
    public class EditUserDto
    {
        public EditUserDto()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string? Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "O nome email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string? UserExtended { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string? Cpf { get; set; }

        //relation
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }

    }
}
