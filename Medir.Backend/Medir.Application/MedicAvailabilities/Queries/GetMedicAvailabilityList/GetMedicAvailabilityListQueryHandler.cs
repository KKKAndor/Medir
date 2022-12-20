using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList
{
    public class GetMedicAvailabilityListQueryHandler
        : IRequestHandler<GetMedicAvailabilityListQuery, MedicAvailabilityListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetMedicAvailabilityListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicAvailabilityListVm> Handle(GetMedicAvailabilityListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.MedicAppointmentAvailabilities
                .Where(m => m.MedicId == request.MedicId)
                .GroupBy(m=> new
                {
                    m.Date,
                    m.PositionId,
                    m.PolyclinicId
                })
                .Select(m=>new MedicAvailabilityLookUpDto
                {
                    Date = m.Key.Date,
                    PolyclinicId = m.Key.PolyclinicId,
                    PositionId = m.Key.PositionId,
                })
                .ToListAsync(cancellationToken);

            List<MedicAvailabilityLookUpDto> list = new List<MedicAvailabilityLookUpDto>();

            foreach (var item in Query)
            {
                var available = await _dbContext.MedicAppointmentAvailabilities.Include(m => m.Position).Include(m => m.Polyclinic)
                    .Where(m =>
                    m.MedicId == request.MedicId &&
                    m.Date == item.Date &&
                    m.PositionId == item.PositionId &&
                    m.PolyclinicId == item.PolyclinicId)
                    .OrderBy(m => m.TimeStart)
                    .ToListAsync(cancellationToken);

                var firstAvailable = available.FirstOrDefault();

                var lastAvailable = available.LastOrDefault();

                list.Add(new MedicAvailabilityLookUpDto
                {
                    PolyclinicName = firstAvailable.Polyclinic.PolyclinicName,
                    PolyclinicId = firstAvailable.PolyclinicId,
                    PositionName = firstAvailable.Position.PositionName,
                    PositionId = firstAvailable.PositionId,
                    TimeEnd = lastAvailable.TimeEnd,
                    TimeStart = firstAvailable.TimeStart,
                    Date = firstAvailable.Date
                });
            }

            list = list.OrderBy(m => m.Date).ToList();

            return new MedicAvailabilityListVm { LookUpList = list };
        }
    }
}
