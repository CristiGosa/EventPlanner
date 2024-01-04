using EventPlanner.Domain.Entities;
using System.Linq.Expressions;

namespace EventPlanner.Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> CreateAsync(Event createdEvent, User organizer);
        Task<IEnumerable<Event>> GetAllAsync(Expression<Func<Event, bool>> predicate);
    }
}
