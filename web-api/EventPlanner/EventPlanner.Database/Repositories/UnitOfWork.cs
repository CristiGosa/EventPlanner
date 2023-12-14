using EventPlanner.Domain.Repositories;
using EventPlanner.Database.Context;

namespace EventPlanner.Database.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
	private readonly DataContext _context;

	public IUserRepository Users { get; }


	public UnitOfWork(IUserRepository userRepository, DataContext context)
	{
		Users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}
}