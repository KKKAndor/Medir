using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Positions.Queries.GetPositionList
{
    public class GetPositionListQueryHandler
        : IRequestHandler<GetPositionListQuery, PositionsListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetPositionListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PositionsListVm> Handle(GetPositionListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.Positions
                .ProjectTo<PositionLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PositionsListVm { Positions = Query };
        }
    }
}
