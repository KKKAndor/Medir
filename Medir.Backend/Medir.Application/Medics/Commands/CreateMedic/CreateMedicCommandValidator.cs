using FluentValidation;

namespace Medir.Application.Medics.Commands.CreateMedic
{
    public class CreateMedicCommandValidator : AbstractValidator<CreateMedicCommand>
    {
        public CreateMedicCommandValidator()
        {
            RuleFor(command =>
                command.MedicFullName).NotEmpty().MaximumLength(100);
            RuleFor(command =>
                command.ShortDescription).NotEmpty().MaximumLength(100);
            RuleFor(command =>
                command.FullDescription).NotEmpty().MaximumLength(250);
            RuleFor(command =>
                command.MedicPhoneNumber).NotEmpty().MaximumLength(25);
            RuleFor(command =>
                command.MedicPhoto).NotEmpty();
        }
    }
}
