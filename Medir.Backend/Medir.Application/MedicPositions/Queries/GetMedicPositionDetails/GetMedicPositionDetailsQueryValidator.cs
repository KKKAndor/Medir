using FluentValidation;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionDetails
{
    public class GetMedicPositionDetailsQueryValidator : AbstractValidator<GetMedicPositionDetailsQuery>
    {
        public GetMedicPositionDetailsQueryValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
        }
    }
}
