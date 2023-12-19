using EventPlanner.Domain.Exceptions.Interfaces;

namespace InVet.Domain.Exceptions;
public class UniqueException : Exception, ITemplatedError
{
	private readonly Type _type;
	
	public UniqueException(Type type)
	{
		_type = type ?? throw new ArgumentNullException(nameof(type));
	}

	public override string Message => $"Entity: {_type.Name} already exists";
	public string ErrorCode => "UniqueType";
	public string MessageDetails => _type.Name!;
}
