using FluentValidation;

namespace Medir.Application.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(command =>
                command.UserEmail).NotEmpty();
            RuleFor(command =>
                command.MedicAppointmentAvailabilityId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.Date).NotEmpty();
            RuleFor(command =>
                command.Time).NotEmpty();
        }
    }
}
