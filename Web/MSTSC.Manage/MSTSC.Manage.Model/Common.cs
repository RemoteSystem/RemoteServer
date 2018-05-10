using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Model
{
    public class Common
    {
        /// <summary>
        /// UI值转数据库值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UIdataToDB(string value)
        {
            string retValue = value;

            if (value == "三分类")
            {
                retValue = "3diff";
            }

            if (value == "五分类")
            {
                retValue = "5diff";
            }

            if(value=="开放")
            {
                retValue = "open";
            }

            if(value=="封闭")
            {
                retValue = "close";
            }

            return retValue;
        }

        /// <summary>
        /// 数据库值转UI值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DBdataToUI(string value)
        {
            string retValue = value;
            if (value == "3diff")
            {
                retValue = "三分类";
            }

            if (value == "5diff")
            {
                retValue = "五分类";
            }

            if (value == "open")
            {
                retValue = "开放";
            }

            if (value == "close")
            {
                retValue = "封闭";
            }

            return retValue;
        }
    }
}
