using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace na.admin.Models
{
    public class Result
    {
        public Result()
        {
        }
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }//End of Class
}//End of Namespace