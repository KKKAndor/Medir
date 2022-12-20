using AutoMapper;
using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medir.Application.Positions.Queries.GetPositionDetails
{
    public class GetPositionDetailsQueryHandler
        : IRequestHandler<GetPositionDetailsQuery, PositionDetailsVm>
    {
        private readonly IMedirDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPositionDetailsQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PositionDetailsVm> Handle(GetPositionDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Positions
                .FirstOrDefaultAsync(a =>
                a.PositionId == request.PositionId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Position), request.PositionId);
            }

            return _mapper.Map<PositionDetailsVm>(entity);
        }
    }
}
