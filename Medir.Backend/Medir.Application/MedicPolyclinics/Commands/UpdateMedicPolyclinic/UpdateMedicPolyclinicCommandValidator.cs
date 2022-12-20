using FluentValidation;

namespace Medir.Application.MedicPolyclinics.Commands.UpdateMedicPolyclinic
{
    public class UpdateMedicPolyclinicCommandValidator : AbstractValidator<UpdateMedicPolyclinicCommand>
    {
        public UpdateMedicPolyclinicCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
        }
    }
}
