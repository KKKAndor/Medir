using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.Interfaces;
using Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionList
{
    public class GetMedicPositionListQueryHandler
        : IRequestHandler<GetMedicPositionListQuery, MedicPositionsListVm>, ISearchSort<MedicPositionLookUpDto>
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

            if (request.Parameters != null)
            {
                Search(ref Query, request.Parameters);

                Sort(ref Query, request.Parameters);
            }

            var pagedList = PagedList<MedicPositionLookUpDto>.ToPagedList(
                Query.AsQueryable(),
                request.Parameters.PageNumber,
                request.Parameters.PageSize);

            return new MedicPositionsListVm { MedicPositions = pagedList };
        }
        public void Search(ref List<MedicPositionLookUpDto> list, QueryStringParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Contains))
            {
                list.Where(i =>
                i.PositionName.ToLower().Contains(parameters.Contains.ToLower()));
            }
        }

        public void Sort(ref List<MedicPositionLookUpDto> list, QueryStringParameters parameters)
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
                    case "DateOnPosition":
                        list = list.OrderBy(o => o.DateOnPosition).ToList();
                        break;
                    case "ReverseDateOnPosition":
                        list = list.OrderBy(o => o.DateOnPosition).Reverse().ToList();
                        break;
                    default:
                        list = list.OrderBy(o => o.PositionName).ToList();
                        break;
                }
            }
            
        }
    }
}
