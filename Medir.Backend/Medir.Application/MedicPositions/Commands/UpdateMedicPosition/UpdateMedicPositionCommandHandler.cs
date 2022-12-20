using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Application.Medics.Commands.UpdateMedic;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.MedicPositions.Commands.UpdateMedicPosition
{
    public class UpdateMedicPositionCommandHandler
        : IRequestHandler<UpdateMedicPositionCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public UpdateMedicPositionCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateMedicPositionCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.MedicPositions
                .FirstOrDefaultAsync(a => a.MedicId == request.MedicId && a.PositionId == request.PositionId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MedicPosition), new object[] { request.MedicId, request.PositionId });
            }

            entity.DateOnPosition = request.DateOnPosition;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
