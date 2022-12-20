using FluentValidation;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionList
{
    public class GetMedicPositionListQueryValidator : AbstractValidator<GetMedicPositionListQuery>
    {
        public GetMedicPositionListQueryValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
        }
    }
}
