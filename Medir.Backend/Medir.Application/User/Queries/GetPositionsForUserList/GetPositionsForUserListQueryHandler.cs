using AutoMapper;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.User.Queries.GetPositionsForUserList
{
    public class GetPositionsForUserListQueryHandler
        : IRequestHandler<GetPositionsForUserListQuery, PositionsForUserListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetPositionsForUserListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PositionsForUserListVm> Handle(GetPositionsForUserListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext
                .MedicPolyclinics
                .Include(m=>m.Polyclinic)
                .Join(
                _dbContext.MedicPositions,
                u => u.MedicId,
                c => c.MedicId,
                (u, c) => new
                {
                    PositionId = c.PositionId,
                    MedicId = u.MedicId,
                    CityId = u.Polyclinic.CityId
                })
                .ToListAsync(cancellationToken);

            if (request.CityId != Guid.Empty)
            {
                Query = Query
                    .Where(m => m.CityId == request.CityId)
                    .ToList();
            }

            var finalQuery = Query
                .GroupBy(u => u.PositionId)
                .ToList();

            List<PositionForUserLookUpDto> list = new List<PositionForUserLookUpDto>();

            foreach (var item in finalQuery)
            {
                var name = (await _dbContext.Positions.FindAsync(item.Key))!.PositionName;
                var count = (await _dbContext.MedicPositions.Where(m => m.PositionId == item.Key)
                    .ToListAsync(cancellationToken))
                    .Count();
                list.Add(new PositionForUserLookUpDto
                {
                    PositionId = item.Key,
                    PositionName = name,
                    MedicCount = count
                });
            }

            return new PositionsForUserListVm { PositionsForUserList = list };            
        }
    }
}
