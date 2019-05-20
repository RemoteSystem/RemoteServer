using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class LogConditionModel
    {
        /// <summary>
        /// 仪器类型
        /// </summary>
        public string DeviceType { get; set; }        

        /// <summary>
        /// 仪器型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 装机区域
        /// </summary>
        public string Region { get; set; }

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
        /// 时间开始
        /// </summary>
        public string dtStart { get; set; }

        /// <summary>
        /// 时间结束
        /// </summary>
        public string dtEnd { get; set; }

    }
}
