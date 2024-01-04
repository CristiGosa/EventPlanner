using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.ViewEvent
{
    public sealed class ViewEventResponse
    {
        public IEnumerable<Event> Events { get; set; }
    }
}
