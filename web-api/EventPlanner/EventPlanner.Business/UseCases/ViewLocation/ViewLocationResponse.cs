using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.ViewLocation
{
    public sealed class ViewLocationResponse
    {
        public IEnumerable<Location> Locations { get; set; }
    }
}
