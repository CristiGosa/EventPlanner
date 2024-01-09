using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.ViewEventByStatus
{
    public sealed class ViewEventByStatusResponse
    {
        public IEnumerable<Event> Events { get; set; }
    }
}
