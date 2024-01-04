using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vox.Models
{
    [Table("users")]
    public partial class UserModel : BaseModel
    {
        [Column("first_name")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Column("password")]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
