using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Common
{
    public static class Methods
    {
        public static DateTime FromStringToDateTime(string date)
        {
            var d = date.Split('/');
            return new DateTime(Convert.ToInt16(d[2]), Convert.ToInt16(d[0]), Convert.ToInt16(d[1]));
        }
    }
}
