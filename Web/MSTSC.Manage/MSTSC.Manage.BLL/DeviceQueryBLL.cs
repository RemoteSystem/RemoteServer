using MSTSC.Manage.DAL;
using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.BLL
{
    public class DeviceQueryBLL
    {
        private DeviceManageDAL dal = new DeviceManageDAL();

        public List<QueryList> GetDeviceInfoBLL(QueryConditionModel conditValue)
        {
            conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.GetDeviceInfoDAL(conditValue);
        }

        /// <summary>
        /// 仪器类型信息
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeModel> ProductTypeInfoBLL()
        {
            return dal.ProductTypeInfoDAL();
        }

        /// <summary>
        /// 仪器详细信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public dynamic GetDeviceDetialBLL(string sn)
        {
            return dal.GetDeviceDetialDAL(sn);
        }

        /// <summary>
        /// 快速查询
        /// </summary>
        /// <param name="queryText"></param>
        /// <returns></returns>
        public List<QueryList> QuickQueryBLL(string queryText)
        {
            return dal.QuickQueryDAL(queryText);
        }


        //******************* NEW *****************//
        public List<QueryList> GetDeviceInfoBLL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.GetDeviceInfoDAL(conditValue,pagerInfo, sortInfo);
        }
        public int getDeviceCount(QueryConditionModel conditValue) {
            return dal.getDeviceCount(conditValue);
        }

    }
}
