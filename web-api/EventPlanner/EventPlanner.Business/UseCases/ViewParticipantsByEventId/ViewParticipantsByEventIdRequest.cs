using MediatR;

namespace EventPlanner.Business.UseCases.ViewParticipantsByEventId
{
    public sealed class ViewParticipantsByEventIdRequest : IRequest<ViewParticipantsByEventIdResponse>
    {
        public int EventId { get; set; }
    }
}
