using AutoMapper;
using EventPlanner.Database.Context;
using EventPlanner.Database.Models;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using InVet.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Database.Repositories
{
    internal sealed class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LocationRepository(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Location> CreateAsync(Location location)
        {
            if (!IsUnique(location))
            {
                throw new UniqueException(typeof(Location));
            }

            LocationData locationData = _mapper.Map<LocationData>(location);

            await _context.Locations.AddAsync(locationData);

            return location;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _mapper.ProjectTo<Location>(_context.Locations).ToListAsync();
        }

        private bool IsUnique(Location location)
        {
            if (_context.Locations.Any(p => p.Name == location.Name && p.Id != location.Id))
            {
                return false;
            }

            return true;
        }
    }
}
