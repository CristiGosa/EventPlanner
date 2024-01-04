using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewEvent
{
    public sealed class ViewEventHandler : IRequestHandler<ViewEventRequest, ViewEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewEventResponse> Handle(ViewEventRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Events.GetAllAsync(x => true);

            if (result == null || !result.Any())
            {
                throw new NotFoundException(typeof(Event));
            }

            return new ViewEventResponse() { Events = result.OrderByDescending(x => x.StartDate) };
        }
    }
}
