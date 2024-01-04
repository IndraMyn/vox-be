using Microsoft.EntityFrameworkCore;
using Vox.Data.OrganizersData;
using Vox.Data.Pagination;
using Vox.Data.SportEventData;
using Vox.Models;
using Vox.Utils;

namespace Vox.Services.SportEventsService
{
    public class SportEventService : ISportEventService
    {
        private readonly ILogger<SportEventService> _logger;

        private readonly DBContext _dbContext;

        public SportEventService(ILogger<SportEventService> logger, DBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }
        public async Task<SportEventResponse> create(SportEventUpdateRequest request)
        {

            var organizer = await _dbContext.Organizers.FindAsync(request.OrganizerId);
            if (organizer == null)
            {
                _logger.LogWarning($"Organizer with {request.OrganizerId} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\SportEvent] {request.OrganizerId}",
                    statusCode = 404
                });
            }

            var sportEvents = new SportEventsModel()
            {
                EventDate = request.EventDate,
                EventName = request.EventName,
                EventType = request.EventType,
                OrganizersId = request.OrganizerId
            };

            await _dbContext.AddAsync(sportEvents);
            await _dbContext.SaveChangesAsync();

            return new SportEventResponse()
            {
                ID = sportEvents.ID,
                EventDate = sportEvents.EventDate,
                EventName = sportEvents.EventName,
                EventType = sportEvents.EventType,
                Organizer = new OrganizersResponse()
                {
                    ID = organizer.ID,
                    OrganizersName = organizer.OrganizerName,
                    ImageLocation = organizer.ImageLocation
                }

            };
        }

        public async Task delete(long id)
        {
            var sportEvents = await _dbContext.Sports.FindAsync(id);
            if (sportEvents == null)
            {
                _logger.LogWarning($"Sport Event with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\SportEvent] {id}",
                    statusCode = 404
                });
            }

            _dbContext.Sports.Remove(sportEvents);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BasePaginationResponse<SportEventResponse>> findAll(SportEventsRequest request)
        {
            List<SportEventResponse> list = new();
            List<SportEventsModel> sportEvents = new();

            if (request.OrganizersId == null)
            {
                sportEvents = await _dbContext.Sports.Skip(request.Page - 1).Take(request.PerPage).ToListAsync();
            } 
            else
            {
                sportEvents = await _dbContext.Sports.Where(x => x.OrganizersId == request.OrganizersId).Skip(request.Page - 1).Take(request.PerPage).ToListAsync();
            }

            var total = await _dbContext.Sports.CountAsync();

            foreach (var sportEvent in sportEvents)
            {
                var organizer = await _dbContext.Organizers.FindAsync(sportEvent.OrganizersId);

                list.Add(new()
                {
                    ID = sportEvent.ID,
                    EventDate = sportEvent.EventDate.Date,
                    EventName = sportEvent.EventName,
                    EventType = sportEvent.EventType,
                    Organizer = new OrganizersResponse()
                    {
                        ID = organizer.ID,
                        OrganizersName = organizer.OrganizerName,
                        ImageLocation = organizer.ImageLocation
                    }
                });
            }

            return new BasePaginationResponse<SportEventResponse>()
            {
                Data = list,
                Meta = new Meta()
                {
                    Pagination = new Pagination()
                    {
                        Count = list.Count,
                        CurrentPage = request.Page,
                        PerPage = request.PerPage,
                        Total = total,
                        TotalPage = total / request.PerPage,
                        Links = new PaginationLinks()
                        {
                            Next = $"/api/v1/sport-events?page={request.Page + 1}&perPage={request.PerPage}"
                        }
                    }
                }
            };
        }

        public async Task<SportEventResponse> findById(long id)
        {
            var sportEvents = await _dbContext.Sports.FindAsync(id);
            if (sportEvents == null)
            {
                _logger.LogWarning($"Sport Event  with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\SportEvent] {id}",
                    statusCode = 404
                });
            }

            var organizer = await _dbContext.Organizers.FindAsync(sportEvents.OrganizersId);

            return new SportEventResponse()
            {
                ID = sportEvents.ID,
                EventDate = sportEvents.EventDate.Date,
                EventName = sportEvents.EventName,
                EventType = sportEvents.EventType,
                Organizer = new OrganizersResponse()
                {
                    ID = organizer.ID,
                    OrganizersName = organizer.OrganizerName,
                    ImageLocation = organizer.ImageLocation
                }

            };
        }

        public async Task<SportEventResponse> update(long id, SportEventUpdateRequest request)
        {
            var sportEvents = await _dbContext.Sports.FindAsync(id);
            if (sportEvents == null)
            {
                _logger.LogWarning($"Sport Event with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\SportEvent] {id}",
                    statusCode = 404
                });
            }

            sportEvents.EventType = request.EventType;
            sportEvents.EventName = request.EventName;
            sportEvents.EventDate = request.EventDate;
            sportEvents.OrganizersId = request.OrganizerId;

            _dbContext.Sports.Update(sportEvents);
            await _dbContext.SaveChangesAsync();

            var organizer = await _dbContext.Organizers.FindAsync(sportEvents.OrganizersId);

            return new SportEventResponse()
            {
                ID = sportEvents.ID,
                EventDate = sportEvents.EventDate.Date,
                EventName = sportEvents.EventName,
                EventType = sportEvents.EventType,
                Organizer = new OrganizersResponse()
                {
                    ID = organizer.ID,
                    OrganizersName = organizer.OrganizerName,
                    ImageLocation = organizer.ImageLocation
                }

            };
        }
    }
}
