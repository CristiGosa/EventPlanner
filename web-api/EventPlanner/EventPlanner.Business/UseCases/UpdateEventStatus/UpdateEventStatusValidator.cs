using FluentValidation;

namespace EventPlanner.Business.UseCases.UpdateEventStatus
{
    public sealed class UpdateEventStatusValidator : AbstractValidator<UpdateEventStatusRequest>
    {
        public UpdateEventStatusValidator()
        {
            RuleFor(x => x.NewStatus).IsInEnum().WithErrorCode("InvalidStatus");
        }
    }
}
