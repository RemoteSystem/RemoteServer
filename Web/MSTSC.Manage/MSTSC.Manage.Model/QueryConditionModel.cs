using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class QueryConditionModel
    {
        /// <summary>
        /// 仪器类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryText { get; set; }

        /// <summary>
        /// 查询设备状态
        /// </summary>
        public MachineState DeviceState { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductSeries { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string ModelType { get; set; }

        /// <summary>
        /// OEM代号
        /// </summary>
        public string OEM { get; set; }

        /// <summary>
        /// 代理商代号
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// 试剂封闭类型
        /// </summary>
        public string ReagentType { get; set; }

        /// <summary>
        /// 装机区域
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 查询设备状态
        /// </summary>
        public string QueryRange { get; set; }
    }
}
