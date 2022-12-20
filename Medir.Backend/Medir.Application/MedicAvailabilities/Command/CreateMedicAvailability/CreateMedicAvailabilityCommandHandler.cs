using MediatR;
using Medir.Application.Interfaces;
using Medir.Application.MedicPositions.Commands.CreateMedicPosition;
using Medir.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.MedicAvailabilities.Command.CreateMedicAvailability
{
    public class CreateMedicAvailabilityCommandHandler
        : IRequestHandler<CreateMedicAvailabilityCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public CreateMedicAvailabilityCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateMedicAvailabilityCommand request,
            CancellationToken cancellationToken)
        {      
            List<MedicAppointmentAvailability> MedicAppointmentAvailabilities = new List<MedicAppointmentAvailability>();

            for (DateTime start = request.TimeStart;
                start + new TimeSpan(0, request.ReceptionMinutesTime, 0)  <= request.TimeEnd;
                start += new TimeSpan(0, request.ReceptionMinutesTime, 0))
            {
                MedicAppointmentAvailabilities.Add(new MedicAppointmentAvailability
                {
                    MedicAppointmentAvailabilityId = new Guid(),
                    AppointmentId = Guid.Empty,
                    IsAvailable = true,
                    TimeStart = start,
                    TimeEnd = start + new TimeSpan(0, request.ReceptionMinutesTime, 0),
                    Date = request.Date,
                    MedicId = request.MedicId,
                    PolyclinicId = request.PolyclinicId,
                    PositionId = request.PositionId
                });
            }

            await _dbContext.MedicAppointmentAvailabilities.AddRangeAsync(MedicAppointmentAvailabilities, cancellationToken);
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
