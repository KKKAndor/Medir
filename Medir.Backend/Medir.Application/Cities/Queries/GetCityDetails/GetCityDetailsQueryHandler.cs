using AutoMapper;
using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Cities.Queries.GetCityDetails
{
    public class GetCityDetailsQueryHandler
        : IRequestHandler<GetCityDetailsQuery, CityDetailsVm>
    {
        private readonly IMedirDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCityDetailsQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<CityDetailsVm> Handle(GetCityDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Cities
                .FirstOrDefaultAsync(a =>
                a.CityId == request.CityId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.CityId);
            }

            return _mapper.Map<CityDetailsVm>(entity);
        }
    }
}
