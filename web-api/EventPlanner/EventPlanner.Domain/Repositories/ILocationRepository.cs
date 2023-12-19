using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories
{
    public interface ILocationRepository
    {
        Task<Location> CreateAsync(Location location);
        Task<IEnumerable<Location>> GetAllAsync();
    }
}
