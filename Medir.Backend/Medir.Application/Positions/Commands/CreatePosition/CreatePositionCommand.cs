using MediatR;

namespace Medir.Application.Positions.Commands.CreatePosition
{
    public class CreatePositionCommand : IRequest<Guid>
    {
        public string PositionName { get; set; } = string.Empty;
    }
}
