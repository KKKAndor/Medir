using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand : IRequest
    {
        public Guid PositionId { get; set; }

        public string PositionName { get; set; } = string.Empty;
    }
}
