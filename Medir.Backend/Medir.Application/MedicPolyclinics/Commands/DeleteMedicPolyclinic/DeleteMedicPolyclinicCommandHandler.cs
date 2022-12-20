using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.MedicPolyclinics.Commands.DeleteMedicPolyclinic
{
    public class DeleteMedicPolyclinicCommandHandler
        : IRequestHandler<DeleteMedicPolyclinicCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public DeleteMedicPolyclinicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteMedicPolyclinicCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.MedicPolyclinics
                .FindAsync(new object[] { request.MedicId, request.PolyclinicId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MedicPolyclinic), new object[] { request.MedicId, request.PolyclinicId });
            }

            _dbContext.MedicPolyclinics.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
