using Vox.Data.Pagination;

namespace Vox.Data.SportEventData
{
    public class SportEventsRequest : BasePaginationRequest
    {
        public long? OrganizersId { get; set; }
    }
}
