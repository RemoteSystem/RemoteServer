using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteModel
{
    public class JsonInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string encoding { get; set; }
        /// <summary>
        /// 设备序列号，是设备的id
        /// </summary>
        public string sn { get; set; }
        /// <summary>
        /// 设备上网卡的卡号
        /// </summary>
        public string sim { get; set; }
        /// <summary>
        /// 设备型号,和model_type相同
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
        /// <summary>
        /// 
        /// </summary>
        public Dump dump { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Category category { get; set; }
    }

    public class Dump
    {
        /// <summary>
        /// 
        /// </summary>
        public string encoding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string data { get; set; }
    }

    public class Runtime
    {
        /// <summary>
        /// 
        /// </summary>
        public string runtime_DAYS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double runtime_POWER { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double runtime_OPT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double runtime_AIR_SUPPLY { get; set; }
    }

    public class Count_statistics
    {
        /// <summary>
        /// 
        /// </summary>
        public string count_times_TOTAL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_WB_CBC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_WB_CD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_WB_CRP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_WB_CBC_CRP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_WB_CD_CRP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_PD_CBC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_PD_CD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_PD_CRP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_PD_CBC_CRP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_PD_CD_CRP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count_times_QC { get; set; }
    }

    public class Reagent
    {
        /// <summary>
        /// 
        /// </summary>
        public string reagent_DIL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_LH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_R1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_R2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_DIFF1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_DIFF2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_FL1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_FL2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_FL3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_FL4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_FL5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_FL6 { get; set; }
    }

    public class FaultItem
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

    public class Module_statistics
    {
        /// <summary>
        /// 
        /// </summary>
        public string hole_times_WBC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hole_times_RBC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string needle_times_impale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sampling_times_fault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Syringe_times_syringe_fault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inject_times_fault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mixing_times_fault { get; set; }
    }

    public class BLOOD
    {
        /// <summary>
        /// 
        /// </summary>
        public string series { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string product_model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string model_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OEM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reagent_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime date_factory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime date_install { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string soft_main_version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime update_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Runtime runtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Count_statistics count_statistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Reagent reagent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FaultItem> fault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Module_statistics module_statistics { get; set; }
    }

    public class Category
    {
        /// <summary>
        /// 
        /// </summary>
        public BLOOD BLOOD { get; set; }
    }

}
