using FluentValidation;

namespace Medir.Application.Positions.Queries.GetPositionDetails
{
    public class GetPositionDetailsQueryValidator : AbstractValidator<GetPositionDetailsQuery>
    {
        public GetPositionDetailsQueryValidator()
        {
            RuleFor(command =>
               command.PositionId).NotEqual(Guid.Empty);
        }
    }
}
