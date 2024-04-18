using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewParticipantsByEventId
{
    internal class ViewParticipantsByEventIdHandler : IRequestHandler<ViewParticipantsByEventIdRequest, ViewParticipantsByEventIdResponse>

    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewParticipantsByEventIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewParticipantsByEventIdResponse> Handle(ViewParticipantsByEventIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.EventReservations.GetParticipantById(request.EventId);

            if (result == null || !result.Any())
            {
                throw new NotFoundException(typeof(Event));
            }

            return new ViewParticipantsByEventIdResponse() { Participants = result.OrderBy(x => x.Name) };
        }
    }
}
