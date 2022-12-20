using Medir.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Interfaces
{
    public interface ISearchSort<T>
    {
        public void Sort(ref List<T> list, QueryStringParameters parameters);

        public void Search(ref List<T> list, QueryStringParameters parameters);
    }
}
