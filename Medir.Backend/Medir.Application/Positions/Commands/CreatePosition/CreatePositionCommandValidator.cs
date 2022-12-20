using FluentValidation;

namespace Medir.Application.Positions.Commands.CreatePosition
{
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionCommandValidator()
        {
            RuleFor(command =>
                command.PositionName).NotEmpty().MaximumLength(250);
        }
    }
}
