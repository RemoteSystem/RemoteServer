using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    /// <summary>
    /// producttype_info表模型
    /// </summary>
    public class ProductTypeModel
    {
        /// <summary>
        /// 仪器类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductSeries { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductModel { get; set; }
    }
}
