using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.Web
{
    public partial class DeviceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <returns>指定对象的集合</returns>
        [WebMethod]
        public static string getDeviceList(int rows, int page, string sort, string sortOrder)
        {
            DeviceQueryBLL bll = new DeviceQueryBLL();

            string where = "864881027507605";
            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = page;
            pagerInfo.PageSize = rows;
            SortInfo sortInfo = new SortInfo(sort, sortOrder);

            List<QueryList> list = new List<QueryList>();
            if (sort != null && !string.IsNullOrEmpty(sortInfo.SortName))
            {
                //list = baseBLL.FindWithPager(where, pagerInfo, sortInfo.SortName, sortInfo.IsDesc);
            }
            else
            {
                list = bll.QuickQueryBLL(where);
                pagerInfo.RecordCount = list.Count;
            }

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return JsonConvert.SerializeObject(result).Replace("null","\"\"");
        }

    }
}