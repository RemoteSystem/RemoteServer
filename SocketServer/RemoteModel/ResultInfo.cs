using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class ResultInfo
    {
        public ResultInfo()
        {
            this.code = 0;
            this.msg = "成功";
        }
        public ResultInfo(int code, string msg)
        {
            this.code = code;
            this.msg = msg;
        }
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
}
