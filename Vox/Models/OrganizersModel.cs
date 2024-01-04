using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vox.Models
{
    [Table("organizers")]
    public partial class OrganizersModel : BaseModel
    {
        [Required]
        [MaxLength(255)]
        [Column("organizers_name")]
        public string OrganizerName { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("image_location")]
        public string ImageLocation { get; set; }

        public List<SportEventsModel> SportEvents { get; set; }
    }
}
