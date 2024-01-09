using EventPlanner.Business.UseCases.ViewEvent;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewApprovedEvent
{
    public sealed class ViewApprovedEventHandler : IRequestHandler<ViewApprovedEventRequest, ViewApprovedEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewApprovedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewApprovedEventResponse> Handle(ViewApprovedEventRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Events.GetAllAsync(x => x.Status == Domain.Enum.EventStatus.Approved);

            if (result == null || !result.Any())
            {
                throw new NotFoundException(typeof(Event));
            }

            return new ViewApprovedEventResponse() { Events = result.OrderByDescending(x => x.StartDate) };
        }
    }
}
