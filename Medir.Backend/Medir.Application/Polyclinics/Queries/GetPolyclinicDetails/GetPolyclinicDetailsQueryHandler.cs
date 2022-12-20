using AutoMapper;
using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicDetails
{
    public class GetPolyclinicDetailsQueryHandler
       : IRequestHandler<GetPolyclinicDetailsQuery, PolyclinicDetailsVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetPolyclinicDetailsQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PolyclinicDetailsVm> Handle(GetPolyclinicDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Polyclinics
                .FirstOrDefaultAsync(a =>
                a.PolyclinicId == request.PolyclinicId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Polyclinic), request.PolyclinicId);
            }

            return _mapper.Map<PolyclinicDetailsVm>(entity);
        }
    }
}
