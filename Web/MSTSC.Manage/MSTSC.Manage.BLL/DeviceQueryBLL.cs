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
        /// 获取仪器最后一次上报的错误信息
        /// </summary>
        /// <param name="sn">仪器序列号</param>
        /// <returns>仪器最后一次上报的错误信息(最多5条)</returns>
        public dynamic GetDeviceFaultBLL(string sn)
        {
            return dal.GetDeviceFaultDAL(sn);
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
            //conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.GetDeviceInfoDAL(conditValue,pagerInfo, sortInfo);
        }
        public int getDeviceCount(QueryConditionModel conditValue) {
            return dal.getDeviceCount(conditValue);
        }

        /// <summary>
        /// 获取产品类型(DeviceType字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getProductType()
        {
            return dal.getProductType();
        }

        /// <summary>
        /// 获取产品系列[3diff、5diff](ProductSeries字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getProductSeries()
        {
            List<KeyValueModel> list = dal.getProductSeries();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].value = Common.DBdataToUI(list[i].value);
            }
            return list;
        }

        /// <summary>
        /// 获取产品型号[Z3、Z30、Z31、Z3CRP、Z30CRP、Z31CRP](Model字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getModel()
        {
            return dal.getModel();
        }

        /// <summary>
        /// 获取产品项目[BK、VK](ProductModel字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getProductModel()
        {
            return dal.getProductModel();
        }

    }
}
