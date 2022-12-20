using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList
{
    public class GetMedicPositionListQueryHandler
        : IRequestHandler<GetMedicPolyclinicListQuery, MedicPolyclinicsListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetMedicPositionListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicPolyclinicsListVm> Handle(GetMedicPolyclinicListQuery request,
            CancellationToken cancellationToken)
        {            
            var Query = await _dbContext.MedicPolyclinics.Include(p => p.Polyclinic)
                .Where(m=>m.MedicId == request.MedicId)
                .ProjectTo<MedicPolyclinicLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MedicPolyclinicsListVm { MedicPolyclinics = Query };
        }
    }
}
