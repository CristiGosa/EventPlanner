using EventPlanner.Domain.Entities;
using System.Linq.Expressions;

namespace EventPlanner.Domain.Repositories
{
    public interface IEventReservationRepository
    {
        Task<EventReservation> CreateAsync(EventReservation createdReservation, User attendee);
        Task<IEnumerable<EventReservation>> GetAllAsync(Expression<Func<EventReservation, bool>> predicate);
    }
}
