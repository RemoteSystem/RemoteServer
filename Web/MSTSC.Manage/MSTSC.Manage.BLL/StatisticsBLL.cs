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

        /// <summary>
        /// 统计-按机型
        /// </summary>
        /// <param name="conditValue"></param>
        /// <param name="pagerInfo"></param>
        /// <param name="sortInfo"></param>
        /// <returns></returns>
        public DataTable StatisticsByTypeBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            //conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.StatisticsByTypeDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getTypeDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getTypeDeviceCount(conditValue);
        }

        /// <summary>
        /// 统计-按OEM
        /// </summary>
        /// <param name="conditValue"></param>
        /// <param name="pagerInfo"></param>
        /// <param name="sortInfo"></param>
        /// <returns></returns>
        public DataTable StatisticsByOEMBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            //conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.StatisticsByOEMDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getOEMDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getOEMDeviceCount(conditValue);
        }

        /***********bio***********/

        public DataTable StatisticsAllBioDevicesBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.StatisticsAllBioDevicesDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getBioDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getBioDeviceCount(conditValue);
        }

        public DataTable BioStatisticsByAreaBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.BioStatisticsByAreaDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getAreaBioDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getAreaBioDeviceCount(conditValue);
        }

        public DataTable BioStatisticsByTypeBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.BioStatisticsByTypeDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getTypeBioDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getTypeBioDeviceCount(conditValue);
        }

        public DataTable StatisticsLogsBLL(LogConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.StatisticsLogsDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getLogCount(LogConditionModel conditValue)
        {
            return dal.getLogCount(conditValue);
        }

        public DataTable StatisticsAllBioDevicesForExportBLL(QueryConditionModel conditValue)
        {
            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = 65530;

            return dal.StatisticsAllBioDevicesDAL(conditValue, pagerInfo, null);
        }

        public DataTable BioStatisticsByAreaForExportBLL(QueryConditionModel conditValue)
        {
            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = 65530;

            return dal.BioStatisticsByAreaDAL(conditValue, pagerInfo, null);
        }

        public DataTable BioStatisticsByTypeForExportBLL(QueryConditionModel conditValue)
        {
            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = 65530;
            return dal.BioStatisticsByTypeDAL(conditValue, pagerInfo, null);
        }

        public DataTable StatisticsLogsForExportBLL(LogConditionModel conditValue)
        {
            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = 65530;
            return dal.StatisticsLogsDAL(conditValue, pagerInfo, null);
        }

    }
}
