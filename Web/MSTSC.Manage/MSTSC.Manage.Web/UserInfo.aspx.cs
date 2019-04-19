using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
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
    }
}