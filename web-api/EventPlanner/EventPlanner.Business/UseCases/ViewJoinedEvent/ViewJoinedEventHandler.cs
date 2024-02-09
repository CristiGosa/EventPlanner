using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewJoinedEvent
{
    public sealed class ViewJoinedEventHandler : IRequestHandler<ViewJoinedEventRequest, ViewJoinedEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewJoinedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewJoinedEventResponse> Handle(ViewJoinedEventRequest request, CancellationToken cancellationToken)
        {
            var reservations = await _unitOfWork.EventReservations.GetAllAsync(x => x.AttendeeEmail == request.AttendeeEmail);

            var result = new List<Event>();
            foreach (var reservation in reservations)
            {
                result.Add(_unitOfWork.Events.GetAllAsync(x => x.Id == reservation.EventId).Result.FirstOrDefault());
            }

            if (result == null || !result.Any())
            {
                throw new NotFoundException(typeof(EventReservation));
            }

            return new ViewJoinedEventResponse() { Events = result };
        }
    }
}
