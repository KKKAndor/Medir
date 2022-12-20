using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.Interfaces;
using Medir.Application.Polyclinics.Queries.GetPolyclinicList;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Positions.Queries.GetPositionList
{
    public class GetPositionListQueryHandler
        : IRequestHandler<GetPositionListQuery, PositionsListVm>, ISearchSort<PositionLookUpDto>
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

            if(request.Parameters != null)
            {
                Search(ref Query, request.Parameters);

                Sort(ref Query, request.Parameters);
            }
            

            var pagedList = PagedList<PositionLookUpDto>.ToPagedList(
               Query.AsQueryable(),
               request.Parameters.PageNumber,
               request.Parameters.PageSize);

            return new PositionsListVm { Positions = pagedList };
        }

        public void Search(ref List<PositionLookUpDto> list, QueryStringParameters parameters)
        {
            var par = parameters as PositionsParameters;

            if (!string.IsNullOrWhiteSpace(par.Contains))
            {
                list = list.Where(ad =>
                ad.PositionName.ToLower().Contains(par.Contains.ToLower())).ToList();
            }

        }

        public void Sort(ref List<PositionLookUpDto> list, QueryStringParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                switch (parameters.OrderBy)
                {
                    case "PositionName":
                        list = list.OrderBy(o => o.PositionName).ToList();
                        break;
                    case "ReversePositionName":
                        list = list.OrderBy(o => o.PositionName).Reverse().ToList();
                        break;
                    case "PositionId":
                        list = list.OrderBy(o => o.PositionId).ToList();
                        break;
                    case "ReversePositionId":
                        list = list.OrderBy(o => o.PositionId).Reverse().ToList();
                        break;
                    default:
                        list = list.OrderBy(o => o.PositionName).ToList();
                        break;
                }
            }
        }
    }
}
