using FluentValidation;

namespace Medir.Application.Cities.Queries.GetCityDetails
{
    public class GetCityDetailsQueryValidator : AbstractValidator<GetCityDetailsQuery>
    {
        public GetCityDetailsQueryValidator()
        {
            RuleFor(command =>
               command.CityId).NotEqual(Guid.Empty);
        }
    }
}
