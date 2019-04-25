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
    public partial class UserRights : BasePage
    {
        [WebMethod]
        public static string saveUserRights(string id, string rights)
        {
            UserBLL bll = new UserBLL();

            string res = "保存成功";
            int n = 0;
            if (!string.IsNullOrEmpty(id))
            {
                rights = rights.Trim(',');
                if (!string.IsNullOrEmpty(rights))
                {
                    n = bll.saveUserRights(id, rights);
                    if (n == 0) res = "保存失败";
                }
            }
            else
            {
                res = "保存失败，未找到用户";
            }

            var retvalue = JsonConvert.SerializeObject(res);
            return retvalue;
        }

        [WebMethod]
        public static string getUserRights(string id)
        {
            UserBLL bll = new UserBLL();
            string result = bll.getUserRights(id);

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

    }
}