using Medir.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.User.Queries.GetMedicsForUserList
{
    public class MedicsForUserListVm
    {
        public PagedList<MedicForUserLookUpDto> MedicsForUser { get; set; }
    }
}
