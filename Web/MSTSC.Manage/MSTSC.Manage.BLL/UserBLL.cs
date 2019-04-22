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

        public int insertUser(User user)
        {
            return dal.insertUser(user);
        }

        public int updateUser(User user)
        {
            return dal.updateUser(user);
        }

        public User getUser(string id)
        {
            return dal.getUser(id);
        }

        public User getUserByUserName(string userName)
        {
            return dal.getUserByUserName(userName);
        }

        public User getUserByUserNameAndPwd(string userName, string pwd)
        {
            return dal.getUserByUserNameAndPwd(userName, pwd);
        }

        public int delUser(string id)
        {
            return dal.delUser(id);
        }

        public int enableUser(string id)
        {
            return dal.enableUser(id);
        }

        public int changePwd(string id,string pwd)
        {
            return dal.changePwd(id,pwd);
        }


    }
}
