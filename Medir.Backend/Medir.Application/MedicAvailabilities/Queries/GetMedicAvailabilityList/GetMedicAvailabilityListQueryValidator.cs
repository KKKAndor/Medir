using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList
{
    public class GetMedicAvailabilityListQueryValidator : AbstractValidator<GetMedicAvailabilityListQuery>
    {
        public GetMedicAvailabilityListQueryValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
        }
    }
}
