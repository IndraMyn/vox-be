using Vox.Data.OrganizersData;

namespace Vox.Data.SportEventData
{
    public class SportEventResponse
    {
        public long ID { get; set; }
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public OrganizersResponse Organizer { get; set; }
    }
}
