using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.User.Queries.GetMedicAvailabiltyForUserList
{
    public class GetMedicAvailabilityForUserListQueryValidator : AbstractValidator<GetMedicAvailabilityForUserListQuery>
    {
        public GetMedicAvailabilityForUserListQueryValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.Date).NotEmpty();
        }
    }
}
