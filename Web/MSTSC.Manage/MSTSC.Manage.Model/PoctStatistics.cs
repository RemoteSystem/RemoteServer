using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class PoctStatistics
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
        /// 样本数
        /// </summary>
        public string smpl { get; set; }

        /// <summary>
        /// 测试卡消耗数，如100
        /// </summary>
        public string card_consume { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        // public DateTime dtinsert { get; set; }
    }
}
