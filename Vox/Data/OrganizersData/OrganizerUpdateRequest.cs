using System.ComponentModel.DataAnnotations;

namespace Vox.Data.OrganizersData
{
    public class OrganizerUpdateRequest
    {
        [Required]
        public string OrganizersName { get; set; }
        [Required]
        public string ImageLocation { get; set; }
    }
}
