using EventPlanner.Domain.Exceptions.Interfaces;

namespace EventPlanner.Domain.Exceptions;
public class InvalidUserException : Exception, ITemplatedError
{
	private const string MessageTemplate = "User not found. User email: {0}";

	private string UserEmail { get; }

	public InvalidUserException(string userEmail)
		: base(string.Format(MessageTemplate, userEmail))
	{
		UserEmail = userEmail;
	}

	public string ErrorCode => "NotFoundUser";
	public string MessageDetails => UserEmail;
}
