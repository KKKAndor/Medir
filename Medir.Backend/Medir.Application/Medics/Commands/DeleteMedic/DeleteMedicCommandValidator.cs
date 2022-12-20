using FluentValidation;

namespace Medir.Application.Medics.Commands.DeleteMedic
{
    public class DeleteMedicCommandValidator : AbstractValidator<DeleteMedicCommand>
    {
        public DeleteMedicCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
        }
    }
}
