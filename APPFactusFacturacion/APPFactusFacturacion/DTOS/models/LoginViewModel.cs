using System.ComponentModel.DataAnnotations;

namespace APPFactusFacturacion.DTOS.models
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
