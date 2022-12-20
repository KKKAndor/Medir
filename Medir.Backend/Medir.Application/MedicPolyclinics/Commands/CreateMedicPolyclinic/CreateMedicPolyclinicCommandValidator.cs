using FluentValidation;

namespace Medir.Application.MedicPolyclinics.Commands.CreateMedicPolyclinic
{
    public class CreateMedicPolyclinicCommandValidator : AbstractValidator<CreateMedicPolyclinicCommand>
    {
        public CreateMedicPolyclinicCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
        }
    }
}
