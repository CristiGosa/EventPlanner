using EventPlanner.Domain.Repositories;
using EventPlanner.Database.Context;

namespace EventPlanner.Database.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
	private readonly DataContext _context;
    public IUserRepository Users { get; }
    public ILocationRepository Locations { get; }
	public IEventRepository Events { get; }
	public IEventReservationRepository EventReservations { get; }


    public UnitOfWork(DataContext context, IUserRepository userRepository, ILocationRepository locationRepository, IEventRepository eventRepository, IEventReservationRepository reservationRepository)
	{
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        Locations = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
		Events = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
		EventReservations = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
	}

	public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}
}