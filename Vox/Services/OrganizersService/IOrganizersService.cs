using Vox.Data.OrganizersData;
using Vox.Data.Pagination;

namespace Vox.Services.OrganizersService
{
    public interface IOrganizersService
    {
        Task<BasePaginationResponse<OrganizersResponse>> findAll(BasePaginationRequest request);
        Task<OrganizersResponse> findById(long id);
        Task<OrganizersResponse> update(long id, OrganizerUpdateRequest request);
        Task  delete(long id);
        Task<OrganizersResponse> create(OrganizerUpdateRequest request);
    }
}
