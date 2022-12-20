using MediatR;

namespace Medir.Application.Polyclinics.Commands.DeletePolyclinic
{
    public class DeletePolyclinicCommand : IRequest
    {
        public Guid PolyclinicId { get; set; }
    }
}
