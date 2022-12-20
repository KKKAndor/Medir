using MediatR;

namespace Medir.Application.User.Queries.GetPositionsForUserList
{
    public class GetPositionsForUserListQuery : IRequest<PositionsForUserListVm>
    {
        public Guid? CityId { get; set; } = Guid.Empty;
    }
}
