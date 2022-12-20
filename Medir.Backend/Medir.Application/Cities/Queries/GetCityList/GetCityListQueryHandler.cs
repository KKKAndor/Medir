using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Cities.Queries.GetCityList
{
    public class GetCityListQueryHandler
        : IRequestHandler<GetCityListQuery, CitiesListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetCityListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<CitiesListVm> Handle(GetCityListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.Cities
                .ProjectTo<CityLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CitiesListVm { Cities = Query };
        }
    }
}
