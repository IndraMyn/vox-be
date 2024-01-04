using System.ComponentModel.DataAnnotations;

namespace Vox.Data.UserData
{
    public class UserUpdateRequests
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MinLength(3, ErrorMessage = "Email can't be less than 3.")]
        public string Email { get; set; }
    }
}
