using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class BioInfo
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
        public BioCategory category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BioDump dump { get; set; }
    }

    public class BioFaultItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime time { get; set; }
    }

    public class BioItemDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public int? blank_time { get; set; }
        /// <summary>
        /// wer李
        /// </summary>
        public string calibration_method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? corrected_intercept { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? corrected_slope { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? first_reagent_volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? k_factor_value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? main_wavelength { get; set; }
        /// <summary>
        /// 未知
        /// </summary>
        public string measuring_method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reaction_direction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? reaction_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? sample_volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? second_reagent_volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? sub_wavelength { get; set; }
    }

    public class StatisticsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public double? R1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? R2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? smpl { get; set; }
    }

    public class Statistics
    {
        /// <summary>
        /// 
        /// </summary>
        public List<StatisticsItem> item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? sample { get; set; }
    }

    public class Bio
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BioFaultItem> fault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BioItemDetail> item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Statistics statistics { get; set; }
    }

    public class BioDump
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

    public class BioCategory
    {
        /// <summary>
        /// 
        /// </summary>
        public Bio bio { get; set; }
    }

}
