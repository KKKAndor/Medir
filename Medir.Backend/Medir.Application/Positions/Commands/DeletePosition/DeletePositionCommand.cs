using MediatR;

namespace Medir.Application.Positions.Commands.DeletePosition
{
    public class DeletePositionCommand : IRequest
    {
        public Guid PositionId { get; set; }
    }
}
