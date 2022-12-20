using FluentValidation;

namespace Medir.Application.MedicPositions.Commands.DeleteMedicPosition
{
    public class DeleteMedicPositionCommandValidator : AbstractValidator<DeleteMedicPositionCommand>
    {
        public DeleteMedicPositionCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
        }
    }
}
