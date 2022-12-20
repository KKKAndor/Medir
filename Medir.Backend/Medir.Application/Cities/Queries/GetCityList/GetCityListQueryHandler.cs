using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Medir.Application.Cities.Queries.GetCityList
{
    public class GetCityListQueryHandler
        : IRequestHandler<GetCityListQuery, CitiesListVm>, ISearchSort<CityLookUpDto>
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

            Search(ref Query, request.Parameters);

            Sort(ref Query, request.Parameters);

            var pagedList = PagedList<CityLookUpDto>.ToPagedList(
                Query.AsQueryable(),
                request.Parameters.PageNumber,
                request.Parameters.PageSize);

            return new CitiesListVm { Cities = pagedList };
        }

        public void Search(ref List<CityLookUpDto> list, QueryStringParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Contains))
                list.Where(i => i.CityName.ToLower().Contains(parameters.Contains.ToLower()));


        }

        public void Sort(ref List<CityLookUpDto> list, QueryStringParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                switch (parameters.OrderBy)

                {
                    case "CityName":
                        list = list.OrderBy(o => o.CityName).ToList();
                        break;
                    case "ReverseCityName":
                        list = list.OrderBy(o => o.CityName).Reverse().ToList();
                        break;                    
                    default:
                        list = list.OrderBy(o => o.CityName).ToList();
                        break;
                }
            }
        }
    }
}
