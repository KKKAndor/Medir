using Medir.Application.Common.Pagination;

namespace Medir.Application.Positions.Queries.GetPositionList
{
    public class PositionsListVm
    {
        public PagedList<PositionLookUpDto> Positions { get; set; }
    }
}
