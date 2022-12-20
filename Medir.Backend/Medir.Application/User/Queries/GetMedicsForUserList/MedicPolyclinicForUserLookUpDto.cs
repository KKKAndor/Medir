using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.User.Queries.GetMedicsForUserList
{
    public class MedicPolyclinicForUserLookUpDto
    {
        public Guid PolyclinicId { get; set; }

        public string PolyclinicName { get; set; } = string.Empty;

        public string PolyclinicAddress { get; set; } = string.Empty;

        public string PolyclinicPhoneNumber { get; set; } = string.Empty;

        public string PolyclinicPhoto { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Price { get; set; }

        public DateTime ClosestAppoint { get; set; }
    }
}
