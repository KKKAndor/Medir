using FluentValidation;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicDetails
{
    public class GetMedicPolyclinicDetailsQueryValidator : AbstractValidator<GetMedicPolyclinicDetailsQuery>
    {
        public GetMedicPolyclinicDetailsQueryValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
        }
    }
}
