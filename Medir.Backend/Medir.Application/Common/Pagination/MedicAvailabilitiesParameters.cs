using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Common.Pagination
{
    public class MedicAvailabilitiesParameters : QueryStringParameters
    {
        public MedicAvailabilitiesParameters()
        {
            OrderBy = "Date";
        }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }
    }
}
