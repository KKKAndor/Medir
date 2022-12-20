using MediatR;
using Medir.Application.Positions.Queries.GetPositionDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Cities.Queries.GetCityDetails
{
    public class GetCityDetailsQuery : IRequest<CityDetailsVm>
    {
        public Guid CityId { get; set; }
    }
}
