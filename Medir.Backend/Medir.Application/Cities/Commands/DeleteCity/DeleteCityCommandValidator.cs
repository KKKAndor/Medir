using FluentValidation;

namespace Medir.Application.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(command =>
                command.CityId).NotEqual(Guid.Empty);
        }
    }
}
