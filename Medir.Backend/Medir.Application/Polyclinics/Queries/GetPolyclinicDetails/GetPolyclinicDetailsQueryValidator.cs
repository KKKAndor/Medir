using FluentValidation;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicDetails
{
    public class GetPolyclinicDetailsQueryValidator : AbstractValidator<GetPolyclinicDetailsQuery>
    {
        public GetPolyclinicDetailsQueryValidator()
        {
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
        }
    }
}
