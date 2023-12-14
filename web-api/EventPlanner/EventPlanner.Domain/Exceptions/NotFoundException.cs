using EventPlanner.Domain.Exceptions.Interfaces;

namespace EventPlanner.Domain.Exceptions;
public sealed class NotFoundException : Exception, ITemplatedError
{
	private readonly Type _entityType;

	private readonly int _id = -1;

	public NotFoundException(Type entityType) : base()
	{
		_entityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
	}

	public NotFoundException(Type entityType, int id) : base()
	{
		_entityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
		_id = id;
	}

	public override string Message => $"No {_entityType.Name}" + (_id != -1 ? $" with id {_id} " : " ") + "has been found.";
	public string ErrorCode => "NoEntityFound";
	public string MessageDetails => _entityType.Name;
}
