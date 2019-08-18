using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class PoctItemDetail
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 测试项目
        /// </summary>
        public string card_name { get; set; }
        /// <summary>
        /// 孵育时间
        /// </summary>
        public string incubate_time { get; set; }
        /// <summary>
        /// 分析项目1名称
        /// </summary>
        public string analyte_1 { get; set; }
        /// <summary>
        /// 分析项目2名称
        /// </summary>
        public string analyte_2 { get; set; }
        /// <summary>
        /// 分析项目3名称
        /// </summary>
        public string analyte_3 { get; set; }
        /// <summary>
        /// 信号值个数
        /// </summary>
        public string signals { get; set; }
        /// <summary>
        /// 测试卡批次
        /// </summary>
        public string card_lot { get; set; }
        /// <summary>
        /// 测试卡有效期
        /// </summary>
        public string expiry { get; set; }
        /// <summary>
        /// 分析项目1参数
        /// </summary>
        public string analyte_1_params { get; set; }
        /// <summary>
        /// 分析项目2参数
        /// </summary>
        public string analyte_2_params { get; set; }
        /// <summary>
        /// 分析项目3参数
        /// </summary>
        public string analyte_3_params { get; set; }
    }
}
