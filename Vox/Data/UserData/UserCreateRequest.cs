using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vox.Data.UserData
{
    public class UserCreateRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MinLength(3, ErrorMessage = "Email can't be less than 3.")]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password can't be less than 8.")]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Repeat Password can't be less than 8.")]
        [PasswordPropertyText]
        public string RepeatPassword { get; set; }
    }
}
