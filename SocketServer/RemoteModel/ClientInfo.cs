using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class ClientInfo
    {
        public int id { get; set; }
        /// <summary>
        /// utf-8
        /// </summary>
        public string encoding { get; set; }
        /// <summary>
        /// z3201801002
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        /// 15812345670
        /// </summary>
        public string sim { get; set; }
        /// <summary>
        /// z3
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 湖北省
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// 湖北省武汉市
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 湖北省中医院
        /// </summary>
        public string hospital { get; set; }
        /// <summary>
        /// session id
        /// </summary>
        public string sessionid { get; set; }
        /// <summary>
        /// session 连接开始时间
        /// </summary>
        public DateTime starttime { get; set; }
    }
}
