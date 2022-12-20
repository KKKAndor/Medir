using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Polyclinics.Commands.UpdatePolyclinic
{
    public class UpdatePolyclinicCommandHandler
        : IRequestHandler<UpdatePolyclinicCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public UpdatePolyclinicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePolyclinicCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Polyclinics.FirstOrDefaultAsync(a =>
                a.PolyclinicId == request.PolyclinicId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Polyclinic), request.PolyclinicId);
            }

            entity.CityId = request.CityId;
            entity.PolyclinicAddress = request.PolyclinicAddress;
            entity.PolyclinicName = request.PolyclinicName;
            entity.PolyclinicPhoneNumber = request.PolyclinicPhoneNumber;
            entity.PolyclinicPhoto = request.PolyclinicPhoto;
            entity.Latitude = request.Latitude;
            entity.Longitude = request.Longitude;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
