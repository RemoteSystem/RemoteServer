using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class QueryList
    {
        /// <summary>
        /// 仪器名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// SIM卡号
        /// </summary>
        public string SIM { get; set; }

        /// <summary>
        /// 仪器序列号
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductSeries { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductModel { get; set; }

        public string SESSIONID { get; set; }

        public string SESSION_ID
        {
            get
            {
                if (!string.IsNullOrEmpty(SESSIONID))
                {
                    return "已连接";
                }
                else
                {
                    return "未连接";
                }
            }
        }
    }
}
