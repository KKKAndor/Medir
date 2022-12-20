using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.MedicPositions.Commands.DeleteMedicPosition
{
    public class DeleteMedicPositionCommandHandler
        : IRequestHandler<DeleteMedicPositionCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public DeleteMedicPositionCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteMedicPositionCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.MedicPositions
                .FindAsync(new object[] { request.MedicId, request.PositionId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MedicPosition), new object[] { request.MedicId, request.PositionId });
            }

            _dbContext.MedicPositions.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
