namespace EventPlanner.Domain.Exceptions.Interfaces;
public interface ISimpleError
{
	string ErrorType => "SimpleError";
	string ErrorCode { get; }
	string Message { get; }
}
