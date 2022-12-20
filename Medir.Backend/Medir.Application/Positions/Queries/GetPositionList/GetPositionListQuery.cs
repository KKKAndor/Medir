using MediatR;
using Medir.Application.Common.Pagination;

namespace Medir.Application.Positions.Queries.GetPositionList
{
    public class GetPositionListQuery : IRequest<PositionsListVm>
    {
        public PositionsParameters Parameters { get; set; } = new();
    }
}
