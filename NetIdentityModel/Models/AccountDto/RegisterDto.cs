using System.ComponentModel.DataAnnotations;

namespace NetIdentityModel.Models.AccountDto
{
    public class RegisterDto
    {
        [Required]
        [Display(Name = "Nome")]
        public string? UserExtended { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string? Cpf { get; set; }




        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ser pelo menos {2} e no máximo {1} caracteres longos.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Password")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string? ConfirmPassword { get; set; }

    }
}
