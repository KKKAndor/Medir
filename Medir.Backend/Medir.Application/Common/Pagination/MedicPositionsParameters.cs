using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Common.Pagination
{
    public class MedicPositionsParameters : QueryStringParameters
    {
        public MedicPositionsParameters()
        {
            OrderBy = "PositionName";
        }
    }
}
