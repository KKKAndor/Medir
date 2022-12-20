using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Medics.Queries.GetMedicList
{
    public class GetMedicListQueryHandler
        : IRequestHandler<GetMedicListQuery, MedicsListVm>
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

            return new MedicsListVm { Medics = Query };
        }
    }
}
