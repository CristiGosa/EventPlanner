using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.ViewApprovedEvent
{
    public sealed class ViewApprovedEventResponse
    {
        public IEnumerable<Event> Events { get; set; }
    }
}
