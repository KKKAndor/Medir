using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandler
        : IRequestHandler<DeleteCityCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public DeleteCityCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteCityCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Cities
                .FindAsync(new object[] { request.CityId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.CityId);
            }

            _dbContext.Cities.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
