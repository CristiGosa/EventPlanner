using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.ViewJoinedEvent
{
    public sealed class ViewJoinedEventResponse
    {
        public IEnumerable<Event> Events { get; set; }
    }
}
