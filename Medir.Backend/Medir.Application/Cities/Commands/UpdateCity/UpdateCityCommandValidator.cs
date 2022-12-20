using FluentValidation;

namespace Medir.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(command =>
                command.CityId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.CityName).NotEmpty().MaximumLength(250);
            RuleFor(command =>
                command.Latitude).NotEmpty();
            RuleFor(command =>
                command.Longitude).NotEmpty();
        }
    }
}
