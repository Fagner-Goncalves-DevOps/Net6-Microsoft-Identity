using System.ComponentModel.DataAnnotations;

namespace NetIdentityModel.Models.AccountDto
{
    public class CreateRoleDto
    {
        [Required]
        [Display(Name = "Role")]
        public string? RoleName { get; set; }
    }
}
