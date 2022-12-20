using MediatR;
using Medir.Application.Common;
using Medir.Application.Interfaces;
using Medir.Application.Medics.Commands.CreateMedic;
using Medir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.MedicPositions.Commands.CreateMedicPosition
{
    public class CreateMedicPositionCommandHandler
        : IRequestHandler<CreateMedicPositionCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public CreateMedicPositionCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateMedicPositionCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new MedicPosition
            {
                MedicId = request.MedicId,
                PositionId = request.PositionId,
                DateOnPosition = request.DateOnPosition
            };

            await _dbContext.MedicPositions.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
