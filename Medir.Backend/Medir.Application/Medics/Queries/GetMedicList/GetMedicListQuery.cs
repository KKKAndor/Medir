using MediatR;
using Medir.Application.Common.Pagination;

namespace Medir.Application.Medics.Queries.GetMedicList
{
    public class GetMedicListQuery : IRequest<MedicsListVm>
    {
        public MedicsParameters Parameters { get; set; } = new();
    }
}
