using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.Interfaces;
using Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList
{
    public class GetMedicPositionListQueryHandler
        : IRequestHandler<GetMedicPolyclinicListQuery, MedicPolyclinicsListVm>, ISearchSort<MedicPolyclinicLookUpDto>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetMedicPositionListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicPolyclinicsListVm> Handle(GetMedicPolyclinicListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.MedicPolyclinics.Include(p => p.Polyclinic)
                .Where(m => m.MedicId == request.MedicId)
                .ProjectTo<MedicPolyclinicLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (request.Parameters != null)
            {
                Search(ref Query, request.Parameters);

                Sort(ref Query, request.Parameters);
            }

            var pagedList = PagedList<MedicPolyclinicLookUpDto>.ToPagedList(
                Query.AsQueryable(),
                request.Parameters.PageNumber,
                request.Parameters.PageSize);

            return new MedicPolyclinicsListVm { MedicPolyclinics = pagedList };
        }

        public void Search(ref List<MedicPolyclinicLookUpDto> list, QueryStringParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Contains))
            {
                list.Where(i =>
            i.PolyclinicName.ToLower().Contains(parameters.Contains.ToLower()));
            }
        }

        public void Sort(ref List<MedicPolyclinicLookUpDto> list, QueryStringParameters parameters)
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
                    case "Price":
                        list = list.OrderBy(o => o.Price).ToList();
                        break;
                    case "ReversePrice":
                        list = list.OrderBy(o => o.Price).Reverse().ToList();
                        break;
                    default:
                        list = list.OrderBy(o => o.PolyclinicName).ToList();
                        break;
                }
            }
        }
    }
}
