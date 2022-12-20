using FluentValidation;

namespace Medir.Application.MedicPolyclinics.Commands.DeleteMedicPolyclinic
{
    public class DeleteMedicPolyclinicCommandValidator : AbstractValidator<DeleteMedicPolyclinicCommand>
    {
        public DeleteMedicPolyclinicCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
        }
    }
}
