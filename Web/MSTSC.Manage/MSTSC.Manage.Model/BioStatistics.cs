using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class BioStatistics
    {
        /// <summary>
        /// 仪器序列号
        /// </summary>
        public string device_sn { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string num { get; set; }

        /// <summary>
        /// R1
        /// </summary>
        public string R1 { get; set; }

        /// <summary>
        /// R2
        /// </summary>
        public string R2 { get; set; }

        /// <summary>
        /// 样本数
        /// </summary>
        public string smpl { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        // public DateTime dtinsert { get; set; }
    }
}
