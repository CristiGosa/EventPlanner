using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewEventByStatus
{
    public sealed class ViewEventByStatusHandler : IRequestHandler<ViewEventByStatusRequest, ViewEventByStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewEventByStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewEventByStatusResponse> Handle(ViewEventByStatusRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Events.GetAllAsync(x => x.Status == request.Status);

            if (result == null || !result.Any())
            {
                throw new NotFoundException(typeof(Event));
            }

            return new ViewEventByStatusResponse() { Events = result.OrderByDescending(x => x.StartDate) };
        }
    }
}
