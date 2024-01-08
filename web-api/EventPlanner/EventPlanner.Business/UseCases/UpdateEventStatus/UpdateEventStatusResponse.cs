using EventPlanner.Domain.Enum;

namespace EventPlanner.Business.UseCases.UpdateEventStatus
{
    public sealed class UpdateEventStatusResponse
    {
        public int EventId { get; set; }
        public EventStatus NewStatus { get; set; }
    }
}
