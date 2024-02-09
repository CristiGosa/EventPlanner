using AutoMapper;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.CreateEventReservation
{
    public sealed class CreateEventReservationHandler : IRequestHandler<CreateEventReservationRequest, EventReservation>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventReservationHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<EventReservation> Handle(CreateEventReservationRequest request, CancellationToken cancellationToken)
        {
            EventReservation createdReservation = _mapper.Map<EventReservation>(request);

            if (IsEventAtFullCapacity(createdReservation))
            {
                throw new FullEventCapacityException();
            }

            User attendee = await _unitOfWork.Users.GetByEmailAsync(request.AttendeeEmail);

            if (attendee == null || isUserAlreadyJoined(request))
            {
                throw new InvalidUserException(request.AttendeeEmail);
            }

            EventReservation result = await _unitOfWork.EventReservations.CreateAsync(createdReservation, attendee);

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }

        private bool IsEventAtFullCapacity(EventReservation reservation)
        {
            Event joinedEvent = _unitOfWork.Events.GetAllAsync(x => x.Id == reservation.EventId).Result.First();

            Location location = _unitOfWork.Locations.GetAllAsync().Result.First(x => x.Id == joinedEvent.LocationId);

            if(joinedEvent.ParticipantsNumber >= location.Capacity)
            {
                return true;
            }

            return false;
        }

        private bool isUserAlreadyJoined(CreateEventReservationRequest request)
        {
            var userReservations = _unitOfWork.EventReservations.GetAllAsync(x => x.AttendeeEmail == request.AttendeeEmail).Result.ToList();
            if (userReservations.Any(x => x.EventId == request.EventId))
            {
                return true;
            }
            return false;
        }
    }
}
