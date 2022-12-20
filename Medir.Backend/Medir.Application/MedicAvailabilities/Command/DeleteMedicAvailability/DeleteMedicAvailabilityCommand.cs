using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.MedicAvailabilities.Command.DeleteMedicAvailability
{
    public class DeleteMedicAvailabilityCommand : IRequest
    {
        public Guid MedicId { get; set; }

        public Guid PolyclinicId { get; set; }

        public Guid PositionId { get; set; }

        public DateTime Date { get; set; }
    }
}
