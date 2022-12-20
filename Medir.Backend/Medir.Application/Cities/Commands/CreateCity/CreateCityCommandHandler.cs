using MediatR;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler
        : IRequestHandler<CreateCityCommand, Guid>
    {
        private readonly IMedirDbContext _dbContext;

        public CreateCityCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCityCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new City
            {
                CityId = Guid.NewGuid(),
                CityName = request.CityName,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            await _dbContext.Cities.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.CityId;
        }
    }
}
