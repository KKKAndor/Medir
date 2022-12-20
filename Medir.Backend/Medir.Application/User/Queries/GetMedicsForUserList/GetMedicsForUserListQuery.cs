using MediatR;
using Medir.Application.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Medir.Application.User.Queries.GetMedicsForUserList
{
    public class GetMedicsForUserListQuery : IRequest<MedicsForUserListVm>
    {
        [Required]
        public Guid PositionId { get; set; }

        [Required]
        public Guid CityId { get; set; }

        public MedicsForUserParameters? Parameters { get; set; } = new();
    }
}
