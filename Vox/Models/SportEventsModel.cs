using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vox.Models
{
    [Table("sport_events")]
    public partial class SportEventsModel : BaseModel
    {
        [Required]
        [Column("event_date")]
        public DateTime EventDate { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("event_name")]
        public string EventName { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("event_type")]
        public string EventType { get; set; }

        [Required]
        [Column("organizers_id")]
        [ForeignKey("organizers")]
        public long OrganizersId { get; set; }

        public OrganizersModel Organizers { get; set; }
    }
}
