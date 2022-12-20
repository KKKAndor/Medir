using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.User.Queries.GetMedicsForUserList
{
    public class GetMedicsForUserListQuery : IRequest<MedicsForUserListVm>
    {
        [BindProperty]
        public Guid PositionId { get; set; }

        [BindProperty]
        public Guid CityId { get; set; }
    }
}
