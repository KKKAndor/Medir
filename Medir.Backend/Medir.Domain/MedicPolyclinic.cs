using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Domain
{
    public class MedicPolyclinic
    {
        public Guid MedicId { get; set; }

        public Medic? Medic {get;set;}

        public Guid PolyclinicId { get; set; }

        public Polyclinic? Polyclinic { get; set; }

        public double Price { get; set; }
    }
}
