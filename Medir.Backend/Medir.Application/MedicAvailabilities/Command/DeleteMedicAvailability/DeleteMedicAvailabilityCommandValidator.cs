using FluentValidation;

namespace Medir.Application.MedicAvailabilities.Command.DeleteMedicAvailability
{
    public class DeleteMedicAvailabilityCommandValidator : AbstractValidator<DeleteMedicAvailabilityCommand>
    {
        public DeleteMedicAvailabilityCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.Date).NotEmpty();
        }
    }
}
