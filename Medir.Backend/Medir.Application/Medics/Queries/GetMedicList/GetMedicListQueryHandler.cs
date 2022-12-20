using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Common.Pagination;
using Medir.Application.Interfaces;
using Medir.Application.Polyclinics.Queries.GetPolyclinicList;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Medics.Queries.GetMedicList
{
    public class GetMedicListQueryHandler
        : IRequestHandler<GetMedicListQuery, MedicsListVm>, ISearchSort<MedicLookUpDto>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetMedicListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicsListVm> Handle(GetMedicListQuery request,
            CancellationToken cancellationToken)
        {
            var Query = await _dbContext.Medics
                .ProjectTo<MedicLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (request.Parameters != null)
            {
                Search(ref Query, request.Parameters);

                Sort(ref Query, request.Parameters);
            }

            var pagedList = PagedList<MedicLookUpDto>.ToPagedList(
                Query.AsQueryable(),
                request.Parameters.PageNumber,
                request.Parameters.PageSize);

            return new MedicsListVm { Medics = pagedList };
        }

        public void Search(ref List<MedicLookUpDto> list, QueryStringParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Contains))
            {
                list = list.Where(i =>
                i.MedicFullName.ToLower().Contains(parameters.Contains.ToLower()) ||
                i.ShortDescription.ToLower().Contains(parameters.Contains.ToLower())).ToList();
            }

        }

        public void Sort(ref List<MedicLookUpDto> list, QueryStringParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                switch (parameters.OrderBy)
                {
                    case "MedicFullName":
                        list = list.OrderBy(o => o.MedicFullName).ToList();
                        break;
                    case "ReverseMedicFullName":
                        list = list.OrderBy(o => o.MedicFullName).Reverse().ToList();
                        break;
                    case "ShortDescription":
                        list = list.OrderBy(o => o.ShortDescription).ToList();
                        break;
                    case "ReverseShortDescription":
                        list = list.OrderBy(o => o.ShortDescription).Reverse().ToList();
                        break;
                    case "PolyclinicId":
                        list = list.OrderBy(o => o.MedicId).ToList();
                        break;
                    case "ReversePolyclinicId":
                        list = list.OrderBy(o => o.MedicId).Reverse().ToList();
                        break;
                    default:
                        list = list.OrderBy(o => o.MedicFullName).ToList();
                        break;
                }
            }
        }
    }
}
