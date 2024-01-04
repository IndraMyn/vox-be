using Vox.Data.OrganizersData;
using Vox.Data.Pagination;
using Vox.Data.SportEventData;

namespace Vox.Services.SportEventsService
{
    public interface ISportEventService
    {
        Task<BasePaginationResponse<SportEventResponse>> findAll(SportEventsRequest request);
        Task<SportEventResponse> findById(long id);
        Task<SportEventResponse> update(long id, SportEventUpdateRequest request);
        Task delete(long id);
        Task<SportEventResponse> create(SportEventUpdateRequest request);
    }
}
