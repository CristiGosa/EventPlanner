using EventPlanner.Domain.Exceptions.Interfaces;

namespace EventPlanner.Domain.Exceptions;
public sealed class InvalidRefreshTokenException : Exception, ITemplatedError
{
	private const string MessageTemplate = "Invalid Refresh Token";

	public InvalidRefreshTokenException() : base(MessageTemplate)
	{

	}

	public string ErrorCode => "InvalidRefreshToken";
	public string MessageDetails => "Invalid Refresh Token";
}
