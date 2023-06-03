using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace na.admin.Utility
{
    public class PagedModel<T>
    {
        public IEnumerable<T> Models { get; set; }
        public PagedList PagedList { get; set; }
        public int TotalEntity { get; set; }
        public int FirstSerialNumber { get; set; }
        public List<ActionViewModel> action { get;set; }
}
}
