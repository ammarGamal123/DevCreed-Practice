using System.ComponentModel.DataAnnotations;

namespace TestApiJwt.Models
{
    public class AddRoleModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string RoleName { get; set; }

    }
}
