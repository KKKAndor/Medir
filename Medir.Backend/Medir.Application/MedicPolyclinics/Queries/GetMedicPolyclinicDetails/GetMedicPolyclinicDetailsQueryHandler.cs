using AutoMapper;
using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicDetails
{
    public class GetMedicPolyclinicDetailsQueryHandler
        : IRequestHandler<GetMedicPolyclinicDetailsQuery, MedicPolyclinicDetailsVm>
    {
        private readonly IMedirDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMedicPolyclinicDetailsQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicPolyclinicDetailsVm> Handle(GetMedicPolyclinicDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.MedicPolyclinics.Include(p=>p.Polyclinic)
                .FirstOrDefaultAsync(a =>
                a.MedicId == request.MedicId && a.PolyclinicId == request.PolyclinicId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MedicPolyclinic), new object[] { request.MedicId, request.PolyclinicId });
            }

            return _mapper.Map<MedicPolyclinicDetailsVm>(entity);
        }
    }
}
