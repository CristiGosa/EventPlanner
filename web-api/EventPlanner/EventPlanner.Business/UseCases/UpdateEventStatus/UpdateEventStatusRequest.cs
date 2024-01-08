using EventPlanner.Domain.Enum;
using MediatR;

namespace EventPlanner.Business.UseCases.UpdateEventStatus
{
    public sealed class UpdateEventStatusRequest : IRequest<UpdateEventStatusResponse>
    {
        public int EventId { get; set; }
        public EventStatus NewStatus { get; set; }
    }
}
