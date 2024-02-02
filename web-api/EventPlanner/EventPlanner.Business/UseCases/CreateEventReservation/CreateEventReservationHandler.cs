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
                throw new Exception("Event is at full capacity");
            }

            User attendee = await _unitOfWork.Users.GetByEmailAsync(request.AttendeeEmail);

            if (attendee == null)
            {
                throw new InvalidUserException(request.AttendeeEmail);
            }

            EventReservation result = await _unitOfWork.EventReservations.CreateAsync(createdReservation, attendee);

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }

        private bool IsEventAtFullCapacity(EventReservation reservation)
        {
            Event joinedEvent = _unitOfWork.Events.GetAllAsync(x => x.Id == reservation.Event.Id).Result.First();

            if(joinedEvent.ParticipantsNumber >= joinedEvent.Location.Capacity)
            {
                return true;
            }

            return false;
        }
    }
}
