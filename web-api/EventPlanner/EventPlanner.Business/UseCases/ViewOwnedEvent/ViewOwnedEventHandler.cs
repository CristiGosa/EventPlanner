using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewOwnedEvent
{
    public sealed class ViewOwnedEventHandler : IRequestHandler<ViewOwnedEventRequest, ViewOwnedEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewOwnedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewOwnedEventResponse> Handle(ViewOwnedEventRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Events.GetAllAsync(x => x.OrganizerEmail == request.OrganizerEmail);

            if (result == null || !result.Any())
            {
                throw new NotFoundException(typeof(Event));
            }

            return new ViewOwnedEventResponse() { Events = result.OrderByDescending(x => x.StartDate) };
        }
    }
}
