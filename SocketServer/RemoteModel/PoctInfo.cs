using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class PoctInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string sn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sim { get; set; }
        /// <summary>
        /// 长沙市
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encoding { get; set; }
        /// <summary>
        /// 第四人民医院
        /// </summary>
        public string hospital { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? machine_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 湖南省
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string request { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime update_time { get; set; }
        /// <summary>
        /// session id
        /// </summary>
        public string sessionid { get; set; }
        /// <summary>
        /// session 连接开始时间
        /// </summary>
        public DateTime starttime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PoctCategory category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PoctDump dump { get; set; }
    }

    public class PoctDump
    {
        /// <summary>
        /// 
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encoding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filename { get; set; }
    }

    public class PoctCategory
    {
        /// <summary>
        /// 
        /// </summary>
        public Poct poct { get; set; }
    }

    public class Poct
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PoctItem> item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PoctStatistics statistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PoctFault> fault { get; set; }
    }

    public class PoctItem
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

    public class PoctStatisticsItem
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 样本数，如100
        /// </summary>
        public string smpl { get; set; }
        /// <summary>
        /// 测试卡消耗数，如100
        /// </summary>
        public string card_consume { get; set; }
    }

    public class PoctStatistics
    {
        /// <summary>
        /// 仪器已测试样本总量
        /// </summary>
        public string sample { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PoctStatisticsItem> item { get; set; }
    }

    public class PoctFault
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
    }

}
