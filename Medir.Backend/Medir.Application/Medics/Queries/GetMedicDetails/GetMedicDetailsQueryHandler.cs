using AutoMapper;
using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Medics.Queries.GetMedicDetails
{
    public class GetMedicDetailsQueryHandler
        : IRequestHandler<GetMedicDetailsQuery, MedicDetailsVm>
    {
        private readonly IMedirDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMedicDetailsQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicDetailsVm> Handle(GetMedicDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Medics
                .FirstOrDefaultAsync(a =>
                a.MedicId == request.MedicId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Medic), request.MedicId);
            }

            return _mapper.Map<MedicDetailsVm>(entity);
        }
    }
}
