using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using MSTSC.Manage.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.Web
{
    public partial class UserInfo : BasePage
    {
        [WebMethod]
        public static string getUserList(string conditions, int rows, int page, string sort, string sortOrder)
        {
            if (rows == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }

            UserBLL bll = new UserBLL();

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = page;
            pagerInfo.PageSize = rows;

            SortInfo sortInfo = new SortInfo(sort, sortOrder);
            UserQueryCondition conditionModel = JsonConvert.DeserializeObject<UserQueryCondition>(conditions);

            DataTable dt = bll.getUserList(conditionModel, pagerInfo, sortInfo);
            pagerInfo.RecordCount = bll.getUserCount(conditionModel);

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = dt };
            return JsonConvert.SerializeObject(result).Replace("null", "0");
        }

        [WebMethod]
        public static string saveUser(string id, string userName, string name, string sex, string age, string isAdmin)
        {
            UserBLL bll = new UserBLL();

            User user = new User();
            user.id = id;
            user.userName = userName;
            user.name = name;
            user.sex = sex;
            user.age = age;
            user.isAdmin = isAdmin;

            string res = "保存成功";
            int n = 0;
            if (string.IsNullOrEmpty(user.id))
            {
                if (null != bll.getUserByUserName(user.userName))
                {
                    res = "用户名已存在，请重新输入.";
                }
                else
                {
                    user.password = EncyptHelper.MD5("123456");
                    n = bll.insertUser(user);
                    if (n == 0) res = "保存失败";
                }
            }
            else
            {
                n = bll.updateUser(user);
            }

            var retvalue = JsonConvert.SerializeObject(res);
            return retvalue;
        }

        [WebMethod]
        public static string getUser(string id)
        {
            UserBLL bll = new UserBLL();
            var result = bll.getUser(id);

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

        [WebMethod]
        public static string delUser(string id)
        {
            UserBLL bll = new UserBLL();
            var result = bll.delUser(id);

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

        [WebMethod]
        public static string changePwd(string oldPwd, string newPwd)
        {
            UserBLL bll = new UserBLL();

            string result = "修改成功";

            HttpSessionState session = HttpContext.Current.Session;
            string userName = session["username"].ToString();

            oldPwd = EncyptHelper.MD5(oldPwd);
            newPwd = EncyptHelper.MD5(newPwd);

            User user = bll.getUserByUserNameAndPwd(userName, oldPwd);
            if (null != user)
            {
                bll.changePwd(user.id, newPwd);
            }
            else
            {
                result = "原密码不正确，修改失败.";
            }

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

        [WebMethod]
        public static string resetPwd(string id)
        {
            UserBLL bll = new UserBLL();

            string result = "重置成功";

            User user = bll.getUser(id);
            if (null != user)
            {
                string pwd = EncyptHelper.MD5("123456");
                bll.changePwd(user.id, pwd);
            }
            else
            {
                result = "重置失败.";
            }

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

        [WebMethod]
        public static string enableUser(string id)
        {
            UserBLL bll = new UserBLL();

            string result = "启用成功";
            bll.enableUser(id);

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

    }
}