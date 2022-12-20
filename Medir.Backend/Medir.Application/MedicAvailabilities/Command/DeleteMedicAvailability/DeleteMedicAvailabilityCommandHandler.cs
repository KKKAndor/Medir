using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Application.MedicPolyclinics.Commands.DeleteMedicPolyclinic;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.MedicAvailabilities.Command.DeleteMedicAvailability
{
    public class DeleteMedicAvailabilityCommandHandler
        : IRequestHandler<DeleteMedicAvailabilityCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public DeleteMedicAvailabilityCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteMedicAvailabilityCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.MedicAppointmentAvailabilities
                .Where(m =>
                m.PolyclinicId == request.PolyclinicId &&
                m.PositionId == request.PositionId &&
                m.MedicId == request.MedicId &&
                m.Date == request.Date)
                .ToListAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MedicAppointmentAvailability), new object[] { request.MedicId, request.PolyclinicId });
            }

            _dbContext.MedicAppointmentAvailabilities.RemoveRange(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
