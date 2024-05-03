using AutoMapper;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Entities.Email;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using EventPlanner.Domain.Services;
using MediatR;

namespace EventPlanner.Business.UseCases.CreateEvent
{
    public sealed class CreateEventHandler : IRequestHandler<CreateEventRequest, Event>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CreateEventHandler(IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<Event> Handle(CreateEventRequest request, CancellationToken cancellationToken)
        {
            Event createdEvent = _mapper.Map<Event>(request);

            Location location = _unitOfWork.Locations.GetAllAsync().Result.FirstOrDefault(x => x.Id == createdEvent.LocationId);

            if (IsLocationBooked(createdEvent))
            {
                throw new BookedLocationException();
            }

            User organizer = await _unitOfWork.Users.GetByEmailAsync(request.OrganizerEmail);

            if (organizer == null)
            {
                throw new InvalidUserException(request.OrganizerEmail);
            }

            Event result = await _unitOfWork.Events.CreateAsync(createdEvent, organizer);

            await _unitOfWork.CommitAsync(cancellationToken);

            var receiverInfo = new ReceiverInfo
            {
                FirstName = "Cristi Gosa",
                Email = "cristi.gosa11@gmail.com"
            };

            var createdEventInfo = new CreatedEventInfo
            {
                EventName = result.Name,
                LocationName = location.Name,
                Creator = result.OrganizerEmail,
                CreatedDate = DateTime.Now,
            };

            await _emailService.SendCreatedEventNotification(receiverInfo, createdEventInfo);

            return result;
        }

        private bool IsLocationBooked(Event createdEvent)
        {
            Location location = _unitOfWork.Locations.GetAllAsync().Result.FirstOrDefault(x => x.Id == createdEvent.LocationId);

            if(location == null)
            {
                return false;
            }

            List<Event> events = _unitOfWork.Events.GetAllAsync(x => x.LocationId == location.Id).Result.ToList();

            foreach(Event existingEvent in events)
            {
                if(((createdEvent.StartDate <= existingEvent.StartDate && createdEvent.EndDate >= existingEvent.StartDate)
                    || (createdEvent.StartDate >= existingEvent.StartDate && createdEvent.EndDate <= existingEvent.EndDate) 
                    || (createdEvent.StartDate <= existingEvent.EndDate && createdEvent.EndDate >= existingEvent.EndDate)) 
                    && (createdEvent.Id != existingEvent.Id) && existingEvent.Status == Domain.Enum.EventStatus.Approved)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

