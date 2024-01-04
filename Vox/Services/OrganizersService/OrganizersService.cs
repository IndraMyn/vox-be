using Microsoft.EntityFrameworkCore;
using Vox.Data.OrganizersData;
using Vox.Data.Pagination;
using Vox.Models;
using Vox.Utils;

namespace Vox.Services.OrganizersService
{
    public class OrganizersService : IOrganizersService
    {

        private readonly ILogger<OrganizersService> _logger;

        private readonly DBContext _dbContext;

        public OrganizersService(ILogger<OrganizersService> logger, DBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        public async Task<BasePaginationResponse<OrganizersResponse>> findAll(BasePaginationRequest request)
        {
            List<OrganizersResponse> list = new();
            var organizers = await _dbContext.Organizers.Skip(request.Page - 1).Take(request.PerPage).ToListAsync();

            var total = await _dbContext.Organizers.CountAsync();

            foreach (var organizer in organizers)
            {
                list.Add(new()
                {
                    ID = organizer.ID,
                    OrganizersName = organizer.OrganizerName,
                    ImageLocation = organizer.ImageLocation
                });
            }

            return new BasePaginationResponse<OrganizersResponse>()
            {
                Data = list,
                Meta = new Meta()
                {
                    Pagination = new Pagination()
                    {
                        Count= list.Count,
                        CurrentPage= request.Page,
                        PerPage= request.PerPage,
                        Total= total,
                        TotalPage= total / request.PerPage,
                        Links = new PaginationLinks()
                        {
                            Next = $"/api/v1/organizers?page={request.Page + 1}&perPage={request.PerPage}"
                        }
                    }
                }
            };
        }

        public async Task delete(long id)
        {

            var organizer = await _dbContext.Organizers.FindAsync(id);
            if (organizer == null)
            {
                _logger.LogWarning($"Organizer with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\Organizer] {id}",
                    statusCode = 404
                });
            }

            _dbContext.Organizers.Remove(organizer);
            await _dbContext.SaveChangesAsync();

        }
        public async Task<OrganizersResponse> findById(long id)
        {
            var organizer = await _dbContext.Organizers.FindAsync(id);
            if (organizer == null)
            {
                _logger.LogWarning($"Organizer with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\Organizer] {id}",
                    statusCode = 404
                });
            }

            return new OrganizersResponse() 
            { 
                ID = organizer.ID,
                OrganizersName = organizer.OrganizerName,
                ImageLocation = organizer.ImageLocation
            };

        }

        public async Task<OrganizersResponse> update(long id, OrganizerUpdateRequest request)
        {
            var organizer = await _dbContext.Organizers.FindAsync(id);
            if (organizer == null)
            {
                _logger.LogWarning($"Organizer with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\Organizer] {id}",
                    statusCode = 404
                });
            }

            organizer.ImageLocation = request.ImageLocation;
            organizer.OrganizerName = request.OrganizersName;

            _dbContext.Organizers.Update(organizer);
            await _dbContext.SaveChangesAsync();

            return new OrganizersResponse()
            {
                ID = organizer.ID,
                OrganizersName = organizer.OrganizerName,
                ImageLocation = organizer.ImageLocation
            };
        }

        public async Task<OrganizersResponse> create(OrganizerUpdateRequest request)
        {
            var organizer = new OrganizersModel()
            {
                ImageLocation= request.ImageLocation,
                OrganizerName = request.OrganizersName,
            };

            await _dbContext.AddAsync(organizer);
            await _dbContext.SaveChangesAsync();

            return new OrganizersResponse()
            {
                ID = organizer.ID,
                OrganizersName = organizer.OrganizerName,
                ImageLocation = organizer.ImageLocation
            };
        }
    }
}
