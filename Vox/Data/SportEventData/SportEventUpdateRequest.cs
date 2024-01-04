using System.ComponentModel.DataAnnotations;

namespace Vox.Data.SportEventData
{
    public class SportEventUpdateRequest
    {
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public string EventType { get; set; }
        [Required]
        public long OrganizerId { get; set; }
    }
}
