using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Positions.Commands.DeletePosition
{
    public class DeletePositionCommandHandler
        : IRequestHandler<DeletePositionCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public DeletePositionCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePositionCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Positions
                .FindAsync(new object[] { request.PositionId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Position), request.PositionId);
            }

            _dbContext.Positions.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
