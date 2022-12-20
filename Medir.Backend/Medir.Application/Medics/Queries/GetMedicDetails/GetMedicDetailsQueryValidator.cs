using FluentValidation;

namespace Medir.Application.Medics.Queries.GetMedicDetails
{
    public class GetMedicDetailsQueryValidator : AbstractValidator<GetMedicDetailsQuery>
    {
        public GetMedicDetailsQueryValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
        }
    }
}
