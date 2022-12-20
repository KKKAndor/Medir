using FluentValidation;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList
{
    public class GetMedicPolyclinicListQueryValidator : AbstractValidator<GetMedicPolyclinicListQuery>
    {
        public GetMedicPolyclinicListQueryValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
        }
    }
}
