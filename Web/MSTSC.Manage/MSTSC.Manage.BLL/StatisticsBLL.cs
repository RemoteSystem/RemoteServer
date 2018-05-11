using MSTSC.Manage.DAL;
using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.BLL
{
    public class StatisticsBLL
    {
        StatisticsDAL dal = new StatisticsDAL();

        /// <summary>
        /// 统计所有仪器
        /// </summary>
        /// <param name="conditValue"></param>
        /// <param name="pagerInfo"></param>
        /// <param name="sortInfo"></param>
        /// <returns></returns>
        public DataTable StatisticsAllDevicesBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            //conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.StatisticsAllDevicesDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getDeviceCount(conditValue);
        }

        public DataTable StatisticsByModelBLL(QueryConditionModel conditValue)
        {
            conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.StatisticsByModelBLL(conditValue);
        }

        /// <summary>
        /// 按封闭试剂类型统计
        /// </summary>
        /// <param name="conditValue"></param>
        /// <returns></returns>
        public DataTable StatisticsByReagentTypeBLL(QueryConditionModel conditValue)
        {
            return dal.StatisticsByReagentTypeDAL(conditValue);
        }

        /// <summary>
        /// 统计-按区域
        /// </summary>
        /// <param name="conditValue"></param>
        /// <param name="pagerInfo"></param>
        /// <param name="sortInfo"></param>
        /// <returns></returns>
        public DataTable StatisticsByAreaBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            //conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.StatisticsByAreaDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getAreaDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getAreaDeviceCount(conditValue);
        }

    }
}
