using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Common.Pagination
{
    public class PolyclinicsParameters : QueryStringParameters
    {
        public PolyclinicsParameters()
        {
            OrderBy = "PolyclinicName";
        }

        public Guid? CityFilterId { get; set; } = Guid.Empty;
    }
}
