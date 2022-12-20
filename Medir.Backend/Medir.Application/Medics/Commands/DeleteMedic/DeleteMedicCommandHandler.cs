using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.Medics.Commands.DeleteMedic
{
    public class DeleteMedicCommandHandler
        : IRequestHandler<DeleteMedicCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public DeleteMedicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteMedicCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Medics
                .FindAsync(new object[] { request.MedicId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Medic), request.MedicId);
            }

            _dbContext.Medics.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
