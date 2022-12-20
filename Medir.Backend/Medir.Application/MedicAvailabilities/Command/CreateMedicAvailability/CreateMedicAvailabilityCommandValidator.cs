using FluentValidation;
using Medir.Application.MedicPositions.Commands.CreateMedicPosition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.MedicAvailabilities.Command.CreateMedicAvailability
{
    public class CreateMedicAvailabilityCommandValidator : AbstractValidator<CreateMedicAvailabilityCommand>
    {
        public CreateMedicAvailabilityCommandValidator()
        {
            RuleFor(command =>
                command.MedicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PositionId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.PolyclinicId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.ReceptionMinutesTime).GreaterThan(0);
            RuleFor(command =>
                command.TimeStart).NotEmpty();
            RuleFor(command =>
                command.TimeEnd).NotEmpty();
            RuleFor(command =>
                command.Date).NotEmpty();
        }
    }
}
