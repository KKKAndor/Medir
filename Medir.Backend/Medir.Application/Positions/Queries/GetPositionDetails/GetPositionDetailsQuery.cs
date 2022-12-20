using MediatR;

namespace Medir.Application.Positions.Queries.GetPositionDetails
{
    public class GetPositionDetailsQuery : IRequest<PositionDetailsVm>
    {
        public Guid PositionId { get; set; }
    }
}
