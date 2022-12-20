using MediatR;
using Medir.Application.Common.Pagination;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicList
{
    public class GetPolyclinicListQuery : IRequest<PolyclinicsListVm>
    {
        public PolyclinicsParameters Parameters { get; set; } = new();
    }
}
