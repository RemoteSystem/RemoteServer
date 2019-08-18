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

        /// <summary>
        /// 仪器型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 医院地址
        /// </summary>
        public string HosAddr { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string HosName { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// 测试项目
        /// </summary>
        public string Card { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string dtStart { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string dtEnd { get; set; }

    }
}
