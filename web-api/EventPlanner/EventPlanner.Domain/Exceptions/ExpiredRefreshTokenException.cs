using EventPlanner.Domain.Exceptions.Interfaces;

namespace EventPlanner.Domain.Exceptions;
public sealed class ExpiredRefreshTokenException : Exception, ITemplatedError
{
	private const string MessageTemplate = "Expired Refresh Token";

	public ExpiredRefreshTokenException() : base(MessageTemplate)
	{

	}

	public string ErrorCode => "InvalidRefreshToken";
	public string MessageDetails => MessageTemplate;
}
