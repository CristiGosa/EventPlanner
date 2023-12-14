using FluentValidation;
using FluentValidation.Results;

using MediatR;
using MediatR.Pipeline;

namespace EventPlanner.Business.Common.Behavior;
public sealed class ValidationPreProcess<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IBaseRequest
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;

	public ValidationPreProcess(IEnumerable<IValidator<TRequest>> validators)
	{
		_validators = validators;
	}

	public Task Process(TRequest request, CancellationToken cancellationToken)
	{
		if (!_validators.Any())
		{
			return Task.CompletedTask;
		}

		ValidationContext<TRequest> context = new(request);

		ValidationFailure[] errors = _validators
			.Select(x => x.Validate(context))
			.SelectMany(x => x.Errors)
			.Where(x => x != null)
			.Distinct()
			.ToArray();

		return errors.Any() ? throw new ValidationException(errors) : Task.CompletedTask;
	}
}
