using AutoMapper;
using EventPlanner.Database.Context;
using EventPlanner.Database.Models;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventPlanner.Database.Repositories
{
    internal sealed class EventReservationRepository : IEventReservationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EventReservationRepository(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EventReservation> CreateAsync(EventReservation createdReservation, User attendee)
        {
            EventReservationData reservationData = _mapper.Map<EventReservationData>(createdReservation);

            reservationData.Attendee = attendee;

            _context.Events.First(x => x.Id == reservationData.EventId).ParticipantsNumber++;

            await _context.EventReservations.AddAsync(reservationData);

            return createdReservation;
        }

        public async Task<IEnumerable<EventReservation>> GetAllAsync(Expression<Func<EventReservation, bool>> predicate)
        {
            var eventReservations = _mapper.ProjectTo<EventReservation>(_context.EventReservations);

            if (predicate != null)
            {
                eventReservations = eventReservations.Where(predicate);
            }

            return await eventReservations.ToListAsync();
        }
    }
}
