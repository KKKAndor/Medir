using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.MedicPolyclinics.Commands.UpdateMedicPolyclinic
{
    public class UpdateMedicPolyclinicCommandHandler
        : IRequestHandler<UpdateMedicPolyclinicCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public UpdateMedicPolyclinicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateMedicPolyclinicCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.MedicPolyclinics
                .FirstOrDefaultAsync(a =>
                a.MedicId == request.MedicId && a.PolyclinicId == request.PolyclinicId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MedicPolyclinic), new object[] { request.MedicId, request.PolyclinicId });
            }

            entity.Price = request.Price;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
