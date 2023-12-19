namespace EventPlanner.Domain.Repositories;

public interface IUnitOfWork
{
	IUserRepository Users { get; }
	ILocationRepository Locations { get; }
	Task<int> CommitAsync(CancellationToken cancellationToken = default);
}