namespace EventPlanner.Domain.Repositories;

public interface IUnitOfWork
{
	IUserRepository Users { get; }
	Task<int> CommitAsync(CancellationToken cancellationToken = default);
}