using FluentValidation;
using Medir.Application.User.Queries.GetMedicsForUserList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Appointments.Queries.GetAppointmentList
{
    public class GetAppointmentListQueryValidator : AbstractValidator<GetAppointmentListQuery>
    {
        public GetAppointmentListQueryValidator()
        {
            RuleFor(command =>
               command.UserEmail).NotEmpty();
        }
    }
}
