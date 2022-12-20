using FluentValidation;

namespace Medir.Application.Positions.Commands.DeletePosition
{
    public class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
    {
        public DeletePositionCommandValidator()
        {
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
        }
    }
}
