using MSTSC.Manage.DAL;
using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.BLL
{
    public class UserBLL
    {
        UserDAL dal = new UserDAL();

        public DataTable getUserList(UserQueryCondition conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            //conditValue.ProductSeries = Common.UIdataToDB(conditValue.ProductSeries);
            return dal.getUserList(conditValue, pagerInfo, sortInfo);
        }

        public int getUserCount(UserQueryCondition conditValue)
        {
            return dal.getUserCount(conditValue);
        }        

    }
}
