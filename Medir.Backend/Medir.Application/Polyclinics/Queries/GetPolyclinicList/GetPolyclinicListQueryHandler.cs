using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicList
{
    public class GetPolyclinicListQueryHandler
        : IRequestHandler<GetPolyclinicListQuery, PolyclinicsListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetPolyclinicListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PolyclinicsListVm> Handle(GetPolyclinicListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.Polyclinics
                .ProjectTo<PolyclinicLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PolyclinicsListVm { Polyclinics = Query };
        }
    }
}
