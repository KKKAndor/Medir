using FluentValidation;

namespace Medir.Application.Polyclinics.Commands.CreatePolyclinic
{
    public class CreatePolyclinicCommandValidator : AbstractValidator<CreatePolyclinicCommand>
    {
        public CreatePolyclinicCommandValidator()
        {
            RuleFor(command =>
                command.CityId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicName).NotEmpty().MaximumLength(100);
            RuleFor(command =>
                command.PolyclinicAddress).NotEmpty().MaximumLength(100);
            RuleFor(command =>
                command.PolyclinicPhoneNumber).NotEmpty().MaximumLength(25);
            RuleFor(command =>
                command.PolyclinicPhoto).NotEmpty();
            RuleFor(command =>
                command.Latitude).NotEmpty();
            RuleFor(command =>
                command.Longitude).NotEmpty();
        }
    }
}
