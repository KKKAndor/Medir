using MediatR;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.Polyclinics.Commands.CreatePolyclinic
{
    public class CreatePolyclinicCommandHandler
        : IRequestHandler<CreatePolyclinicCommand, Guid>
    {
        private readonly IMedirDbContext _dbContext;

        public CreatePolyclinicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePolyclinicCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Polyclinic
            {
                PolyclinicId = Guid.NewGuid(),
                CityId = request.CityId,
                PolyclinicName = request.PolyclinicName,
                PolyclinicAddress = request.PolyclinicAddress,
                PolyclinicPhoneNumber = request.PolyclinicPhoneNumber,
                PolyclinicPhoto = request.PolyclinicPhoto,
                Latitude = request.Latitude,                
                Longitude = request.Longitude
        };

            await _dbContext.Polyclinics.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.PolyclinicId;
        }
    }
}
