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
            if (conditValue.ProduceModel == "三分类")
            {
                conditValue.ProduceModel = "3diff";
            }
            else if (conditValue.ProduceModel == "五分类")
            {
                conditValue.ProduceModel = "5diff";
            }

            conditValue.ReagentType = Common.UIdataToDB(conditValue.ReagentType);

            return dal.GetDeviceInfoDAL(conditValue);
        }

        public List<ProductTypeModel> ProductTypeInfoBLL()
        {
            return dal.ProductTypeInfoDAL();
        }
    }
}
