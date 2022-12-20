using FluentValidation;

namespace Medir.Application.Polyclinics.Commands.DeletePolyclinic
{
    public class DeletePolyclinicCommandValidator : AbstractValidator<DeletePolyclinicCommand>
    {
        public DeletePolyclinicCommandValidator()
        {
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
        }
    }
}
