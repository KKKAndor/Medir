using FluentValidation;

namespace Medir.Application.Cities.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(command =>
                command.CityName).NotEmpty().MaximumLength(250);
            RuleFor(command =>
                command.Latitude).NotEmpty();
            RuleFor(command =>
                command.Longitude).NotEmpty();
        }
    }
}
