using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandHandler
        : IRequestHandler<UpdateCityCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public UpdateCityCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCityCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Cities.FirstOrDefaultAsync(a =>
                a.CityId == request.CityId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.CityId);
            }

            entity.CityName = request.CityName;
            entity.Longitude = request.Longitude;
            entity.Latitude = request.Latitude;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
