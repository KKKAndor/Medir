using AutoMapper;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.User.Queries.GetMedicsForUserList
{
    public class GetMedicsForUserListQueryHandler
        : IRequestHandler<GetMedicsForUserListQuery, MedicsForUserListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetMedicsForUserListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicsForUserListVm> Handle(GetMedicsForUserListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await
              _dbContext.MedicPolyclinics
              .Include(p => p.Polyclinic)
              .Join(
              _dbContext.MedicPositions
              .Where(m => m.PositionId == request.PositionId),
              u => u.MedicId,
              c => c.MedicId,
              (u, c) => new
              {
                  CityId = u.Polyclinic.CityId,
                  MedicId = u.MedicId
              })
              .ToListAsync(cancellationToken);

            if (request.CityId != Guid.Empty)
            {
                Query = Query.Where(q => q.CityId == request.CityId).ToList();
            }

            var query = Query.GroupBy(m => m.MedicId);

            List<MedicForUserLookUpDto> list = new List<MedicForUserLookUpDto>();

            foreach (var item in query)
            {
                var polyclinicsQuery = await
                    _dbContext.MedicPolyclinics
                    .Include(m => m.Polyclinic)
                    .Where(m => m.MedicId == item.Key)
                    .ToListAsync(cancellationToken);

                List<MedicPolyclinicForUserLookUpDto> medicPolyclinicForUserLookUpDtos = new List<MedicPolyclinicForUserLookUpDto>();

                foreach (var i in polyclinicsQuery)
                {
                    var medicInPolyclinicAvailabilityCheck = await _dbContext
                        .MedicAppointmentAvailabilities
                        .Where(m =>
                            m.MedicId == item.Key &&
                            m.Date.Date > DateTime.Now.Date &&
                            m.IsAvailable == true &&
                            m.PolyclinicId == i.PolyclinicId &&
                            m.PositionId == request.PositionId)
                        .OrderBy(m => m.Date)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (medicInPolyclinicAvailabilityCheck == null)
                        continue;

                    medicPolyclinicForUserLookUpDtos.Add(new MedicPolyclinicForUserLookUpDto
                    {
                        PolyclinicId = i.PolyclinicId,
                        PolyclinicAddress = i.Polyclinic.PolyclinicAddress,
                        PolyclinicName = i.Polyclinic.PolyclinicName,
                        PolyclinicPhoneNumber = i.Polyclinic.PolyclinicPhoneNumber,
                        Latitude = i.Polyclinic.Latitude,
                        Longitude = i.Polyclinic.Longitude,
                        Price = i.Price,
                        PolyclinicPhoto = i.Polyclinic.PolyclinicPhoto,
                        ClosestAppoint = medicInPolyclinicAvailabilityCheck.Date
                    });
                }

                var positionsQuery = await
                    _dbContext.MedicPositions
                    .Include(m => m.Position)
                    .Where(m => m.MedicId == item.Key)
                    .ToListAsync(cancellationToken);

                List<string> positionsArray = new List<string>();

                foreach (var i in positionsQuery)
                {
                    positionsArray.Add(i.Position.PositionName);
                }

                var med = await _dbContext.MedicPositions
                    .Include(m => m.Medic)
                    .Include(m => m.Position)
                    .FirstAsync(m => m.PositionId == request.PositionId && m.MedicId == item.Key, cancellationToken);

                list.Add(new MedicForUserLookUpDto
                {
                    MedicFullName = med.Medic.MedicFullName,
                    ShortDescription = med.Medic.ShortDescription,
                    MedicId = med.Medic.MedicId,
                    FullDescription = med.Medic.FullDescription,
                    MedicPhoneNumber = med.Medic.MedicPhoneNumber,
                    MedicPhoto = med.Medic.MedicPhoto,
                    PositionId = request.PositionId,
                    PositionNames = positionsArray,
                    YearsOnPosition = (DateTime.Now.Year - med.DateOnPosition.Year),
                    MedicPolyclinicForUserLookUpList = medicPolyclinicForUserLookUpDtos
                });                
            }

            list = list.OrderBy(m => m.MedicPolyclinicForUserLookUpList.Count > 0 ? 1 : -1).Reverse().ToList();

            return new MedicsForUserListVm { MedicsForUser = list };
        }
    }
}
