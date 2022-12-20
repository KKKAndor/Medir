using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommandHandler
        : IRequestHandler<UpdatePositionCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public UpdatePositionCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePositionCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Positions.FirstOrDefaultAsync(a =>
                a.PositionId == request.PositionId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Position), request.PositionId);
            }

            entity.PositionName = request.PositionName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
