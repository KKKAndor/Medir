using FluentValidation;

namespace Medir.Application.MedicPositions.Commands.UpdateMedicPosition
{
    public class UpdateMedicPositionCommandValidator : AbstractValidator<UpdateMedicPositionCommand>
    {
        public UpdateMedicPositionCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
        }
    }
}
