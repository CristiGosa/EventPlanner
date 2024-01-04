namespace EventPlanner.Domain.Repositories;

public interface IUnitOfWork
{
	IUserRepository Users { get; }
	ILocationRepository Locations { get; }
	IEventRepository Events { get; }
	Task<int> CommitAsync(CancellationToken cancellationToken = default);
}