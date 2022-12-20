using MediatR;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicDetails
{
    public class GetPolyclinicDetailsQuery : IRequest<PolyclinicDetailsVm>
    {
        public Guid PolyclinicId { get; set; }
    }
}
