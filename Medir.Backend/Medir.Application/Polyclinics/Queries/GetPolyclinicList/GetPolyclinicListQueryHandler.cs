using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicList
{
    public class GetPolyclinicListQueryHandler
        : IRequestHandler<GetPolyclinicListQuery, PolyclinicsListVm>, ISearchSort<PolyclinicLookUpDto>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetPolyclinicListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);        

        public async Task<PolyclinicsListVm> Handle(GetPolyclinicListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = new List<PolyclinicLookUpDto>();
            
            if (request.Parameters != null)
            {
                if (request.Parameters.CityFilterId != Guid.Empty)
                {
                    Query = await _dbContext.Polyclinics
                        .Where(p => p.CityId == request.Parameters.CityFilterId)
                        .ProjectTo<PolyclinicLookUpDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                }
                else
                {
                    Query = await _dbContext.Polyclinics
                        .ProjectTo<PolyclinicLookUpDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                }

                Search(ref Query, request.Parameters);

                Sort(ref Query, request.Parameters);
            }

            var pagedList = PagedList<PolyclinicLookUpDto>.ToPagedList(
                Query.AsQueryable(),
                request.Parameters.PageNumber,
                request.Parameters.PageSize);

            return new PolyclinicsListVm { Polyclinics = pagedList };
        }

        public void Search(ref List<PolyclinicLookUpDto> list, QueryStringParameters parameters)
        {
            var par = parameters as PolyclinicsParameters;

            if (!string.IsNullOrWhiteSpace(par.Contains))
            {
                list = list.Where(ad => 
                ad.PolyclinicName.ToLower().Contains(par.Contains.ToLower()) ||
                ad.PolyclinicAddress.ToLower().Contains(par.Contains.ToLower())).ToList();
            }                

        }

        public void Sort(ref List<PolyclinicLookUpDto> list, QueryStringParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                switch (parameters.OrderBy)
                {
                    case "PolyclinicName":
                        list = list.OrderBy(o => o.PolyclinicName).ToList();
                        break;
                    case "ReversePolyclinicName":
                        list = list.OrderBy(o => o.PolyclinicName).Reverse().ToList();
                        break;
                    case "PolyclinicAddress":
                        list = list.OrderBy(o => o.PolyclinicAddress).ToList();
                        break;
                    case "ReversePolyclinicAddress":
                        list = list.OrderBy(o => o.PolyclinicAddress).Reverse().ToList();
                        break;
                    case "PolyclinicId":
                        list = list.OrderBy(o => o.PolyclinicId).ToList();
                        break;
                    case "ReversePolyclinicId":
                        list = list.OrderBy(o => o.PolyclinicId).Reverse().ToList();
                        break;
                    default:
                        list = list.OrderBy(o => o.PolyclinicName).ToList();
                        break;
                }
            }
        }
    }
}
