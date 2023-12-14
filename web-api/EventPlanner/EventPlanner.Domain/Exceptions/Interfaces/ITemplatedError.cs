namespace EventPlanner.Domain.Exceptions.Interfaces;
public interface ITemplatedError
{
	string ErrorType => "TemplatedError";
	string ErrorCode { get; }
	string Message { get; }
	string MessageDetails { get; }
}
