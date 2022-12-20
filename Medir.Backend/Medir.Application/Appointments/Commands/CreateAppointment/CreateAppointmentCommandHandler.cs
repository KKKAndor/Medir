using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Application.Polyclinics.Commands.CreatePolyclinic;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler
        : IRequestHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IMedirDbContext _dbContext;

        public CreateAppointmentCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                Prescription = request.Prescription,
                UserEmail = request.UserEmail,
                Date = request.Date,
                Time = request.Time,
                MedicAppointmentAvailabilityId = request.MedicAppointmentAvailabilityId
            };           

            var maa =
                await _dbContext.MedicAppointmentAvailabilities
                .FirstOrDefaultAsync(m =>m.MedicAppointmentAvailabilityId == request.MedicAppointmentAvailabilityId,
                cancellationToken);

            if (maa == null || maa.IsAvailable == false)
            {
                throw new NotFoundException(nameof(MedicAppointmentAvailability), new object[] { request.MedicAppointmentAvailabilityId });
            }

            var pol = await _dbContext.MedicPolyclinics.FirstAsync(m => m.MedicId == maa.MedicId && m.PolyclinicId == maa.PolyclinicId, cancellationToken);

            entity.Price = pol.Price;

            await _dbContext.Appointments.AddAsync(entity, cancellationToken);

            maa.AppointmentId = entity.AppointmentId;

            maa.IsAvailable = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.AppointmentId;
        }
    }
}
