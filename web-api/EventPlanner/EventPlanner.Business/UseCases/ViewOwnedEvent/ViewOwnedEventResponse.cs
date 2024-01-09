using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.ViewOwnedEvent
{
    public sealed class ViewOwnedEventResponse
    {
        public IEnumerable<Event> Events { get; set; }
    }
}
