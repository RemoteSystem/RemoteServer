using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class DeviceModel
    {
        /// <summary>
        /// 仪器系列号
        /// </summary>
        public string Device_SN { get; set; }

        /// <summary>
        /// SIM卡编号
        /// </summary>
        public string SIM { get; set; }

        /// <summary>
        /// 仪器型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 医院地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string Hospital { get; set; }
    }
}
