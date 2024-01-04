using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vox.Data.UserData
{
    public class UserChangePasswordRequest
    {
        [Required]
        [MinLength(8, ErrorMessage = "Password can't be less than 8.")]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "New Password can't be less than 8.")]
        [PasswordPropertyText]
        public string NewPassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Repeat Password can't be less than 8.")]
        [PasswordPropertyText]
        public string RepeatPassword { get; set; }
    }
}
