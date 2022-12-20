using FluentValidation;

namespace Medir.Application.MedicPositions.Commands.CreateMedicPosition
{
    public class CreateMedicPositionCommandValidator : AbstractValidator<CreateMedicPositionCommand>
    {
        public CreateMedicPositionCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
        }
    }
}
