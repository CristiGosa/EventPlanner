using FluentValidation;

namespace EventPlanner.Business.UseCases.CreateLocation
{
    public sealed class CreateLocationValidator : AbstractValidator<CreateLocationRequest>
    {
        public CreateLocationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("NotEmpty");
            RuleFor(x => x.Name).Matches("^[^a-z]").WithErrorCode("CapitalCase");
            RuleFor(x => x.PlaceId).NotEmpty().WithErrorCode("NotEmpty");
        }
    }
}
