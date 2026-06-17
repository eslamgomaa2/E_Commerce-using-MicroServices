using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specs
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public long Count { get; private set; }
        public IReadOnlyList<T> Data { get; private set; }

        public Pagination(int pageIndex, int pageSize, long count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
