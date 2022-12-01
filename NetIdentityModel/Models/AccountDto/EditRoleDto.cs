using System.ComponentModel.DataAnnotations;

namespace NetIdentityModel.Models.AccountDto
{
    public class EditRoleDto
    {
        public EditRoleDto()
        {
            Users = new List<string>();
        }

        public string? Id { get; set; }

        [Required(ErrorMessage = "O nome da role é obrigatório")]
        public string? RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
