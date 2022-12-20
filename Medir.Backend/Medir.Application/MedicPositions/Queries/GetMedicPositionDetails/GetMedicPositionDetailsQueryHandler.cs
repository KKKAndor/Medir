using AutoMapper;
using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionDetails
{
    public class GetMedicPositionDetailsQueryHandler
        : IRequestHandler<GetMedicPositionDetailsQuery, MedicPositionDetailsVm>
    {
        private readonly IMedirDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMedicPositionDetailsQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MedicPositionDetailsVm> Handle(GetMedicPositionDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.MedicPositions.Include(m => m.Position)
                .FirstOrDefaultAsync(a =>
                a.MedicId == request.MedicId && a.PositionId == request.PositionId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MedicPosition), new object[] { request.MedicId, request.PositionId });
            }

            return _mapper.Map<MedicPositionDetailsVm>(entity);
        }
    }
}
