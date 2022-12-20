using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.Polyclinics.Commands.DeletePolyclinic
{
    public class DeletePolyclinicCommandHandler
        : IRequestHandler<DeletePolyclinicCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public DeletePolyclinicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePolyclinicCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Polyclinics
                .FindAsync(new object[] { request.PolyclinicId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Polyclinic), request.PolyclinicId);
            }

            _dbContext.Polyclinics.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
