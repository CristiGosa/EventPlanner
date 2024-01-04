using AutoMapper;
using EventPlanner.Database.Context;
using EventPlanner.Database.Models;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventPlanner.Database.Repositories
{
    internal sealed class EventRepository : IEventRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EventRepository(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Event> CreateAsync(Event createdEvent, User user)
        {
            EventData eventData = _mapper.Map<EventData>(createdEvent);

            eventData.Organizer = user;

            _context.Locations.Attach(eventData.Location);

            await _context.Events.AddAsync(eventData);

            return createdEvent;
        }

        public async Task<IEnumerable<Event>> GetAllAsync(Expression<Func<Event, bool>> predicate)
        {
            var events = _mapper.ProjectTo<Event>(_context.Events);

            if (predicate != null)
            {
                events = events.Where(predicate);
            }

            return await events.ToListAsync();
        }
    }
}
