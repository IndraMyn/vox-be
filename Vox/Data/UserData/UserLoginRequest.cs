using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vox.Data.UserData
{
    public class UserLoginRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Email can't be less than 3.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password can't be less than 8.")]
        public string Password { get; set; }
    }
}
