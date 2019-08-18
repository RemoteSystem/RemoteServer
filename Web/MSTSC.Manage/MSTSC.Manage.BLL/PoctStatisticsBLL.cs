using MSTSC.Manage.DAL;
using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.BLL
{
    public class PoctStatisticsBLL
    {
        PoctStatisticsDAL dal = new PoctStatisticsDAL();

        public DataTable StatisticsAllPoctDevicesBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.StatisticsAllPoctDevicesDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getPoctDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getPoctDeviceCount(conditValue);
        }

        public DataTable PoctStatisticsByAreaBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.PoctStatisticsByAreaDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getAreaPoctDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getAreaPoctDeviceCount(conditValue);
        }

        public DataTable PoctStatisticsByTypeBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.PoctStatisticsByTypeDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getTypePoctDeviceCount(QueryConditionModel conditValue)
        {
            return dal.getTypePoctDeviceCount(conditValue);
        }

        public DataTable StatisticsAllPoctBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            return dal.StatisticsAllPoctDAL(conditValue, pagerInfo, sortInfo);
        }

        public int getPoctAllCount(QueryConditionModel conditValue)
        {
            return dal.getPoctAllCount(conditValue);
        }

         public DataTable StatisticsAllPoctDevicesForExportBLL(string model)
        {
            QueryConditionModel conditValue = new QueryConditionModel();
            conditValue.Model = model;

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = 65530;

            return dal.StatisticsAllPoctDevicesDAL(conditValue, pagerInfo, null);
        }

        public DataTable PoctStatisticsByAreaForExportBLL(string model)
        {
            QueryConditionModel conditValue = new QueryConditionModel();
            conditValue.Model = model;

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = 65530;

            return dal.PoctStatisticsByAreaDAL(conditValue, pagerInfo, null);
        }

        public DataTable PoctStatisticsByTypeForExportBLL(string model)
        {
            QueryConditionModel conditValue = new QueryConditionModel();
            conditValue.Model = model;

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = 65530;
            return dal.PoctStatisticsByTypeDAL(conditValue, pagerInfo, null);
        }
               
    }
}
