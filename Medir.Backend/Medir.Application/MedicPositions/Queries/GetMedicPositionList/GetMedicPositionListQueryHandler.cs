using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionList
{
    public class GetMedicPositionListQueryHandler
        : IRequestHandler<GetMedicPositionListQuery, MedicPositionsListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetMedicPositionListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicPositionsListVm> Handle(GetMedicPositionListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.MedicPositions.Include(m => m.Position)
                .Where(m => m.MedicId == request.MedicId)
                .ProjectTo<MedicPositionLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MedicPositionsListVm { MedicPositions = Query };
        }
    }
}
