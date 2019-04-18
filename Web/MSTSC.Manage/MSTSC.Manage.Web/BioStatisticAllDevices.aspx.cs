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
    public partial class BioStatisticAllDevices : BasePage
    {
        [WebMethod]
        public static string getDeviceList(string conditions, int rows, int page, string sort, string sortOrder)
        {
            if (rows == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }

            StatisticsBLL bll = new StatisticsBLL();

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = page;
            pagerInfo.PageSize = rows;

            SortInfo sortInfo = new SortInfo(sort, sortOrder);
            QueryConditionModel conditionModel = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));

            //List<QueryList> list = new List<QueryList>();
            DataTable dt = bll.StatisticsAllBioDevicesBLL(conditionModel, pagerInfo, sortInfo);
            pagerInfo.RecordCount = bll.getBioDeviceCount(conditionModel);

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = dt };
            return JsonConvert.SerializeObject(result).Replace("null", "0");
        }
        
    }
}