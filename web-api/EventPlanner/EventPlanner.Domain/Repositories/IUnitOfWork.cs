namespace EventPlanner.Domain.Repositories;

public interface IUnitOfWork
{
	IUserRepository Users { get; }
	ILocationRepository Locations { get; }
	IEventRepository Events { get; }
	IEventReservationRepository EventReservations { get; }
	Task<int> CommitAsync(CancellationToken cancellationToken = default);
}