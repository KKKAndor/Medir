using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.User.Queries.GetMedicAvailabiltyForUserList
{
    public class GetMedicAvailabilityForUserListQueryHandler
        : IRequestHandler<GetMedicAvailabilityForUserListQuery, MedicAvailabilityForUserListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetMedicAvailabilityForUserListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicAvailabilityForUserListVm> Handle(GetMedicAvailabilityForUserListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.MedicAppointmentAvailabilities.Include(m => m.Position).Include(m => m.Polyclinic)
                .Where(m => 
                    m.MedicId == request.MedicId &&
                    m.PositionId == request.PositionId &&
                    m.PolyclinicId == request.PolyclinicId &&
                    m.Date.Date == request.Date.Date &&
                    m.IsAvailable == true)
                .ProjectTo<MedicAvailabilityForUserLookUpDto>(_mapper.ConfigurationProvider)
                .OrderBy(m => m.Date).ThenBy(m => m.TimeStart)
                .ToListAsync(cancellationToken);

            return new MedicAvailabilityForUserListVm { LookUpList = Query };
        }
    }
}
