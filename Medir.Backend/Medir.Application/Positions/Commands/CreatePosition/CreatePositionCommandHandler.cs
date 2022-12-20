using MediatR;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.Positions.Commands.CreatePosition
{
    public class CreatePositionCommandHandler
        : IRequestHandler<CreatePositionCommand, Guid>
    {
        private readonly IMedirDbContext _dbContext;

        public CreatePositionCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePositionCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Position
            {
                PositionId = Guid.NewGuid(),
                PositionName = request.PositionName
            };

            await _dbContext.Positions.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.PositionId;
        }
    }
}
