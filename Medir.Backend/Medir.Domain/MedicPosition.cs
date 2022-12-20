using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Domain
{
    public class MedicPosition
    {
        public Guid MedicId { get; set; }

        public Medic? Medic { get; set; }

        public Guid PositionId { get; set; }

        public Position? Position { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOnPosition { get; set; }        
    }
}
