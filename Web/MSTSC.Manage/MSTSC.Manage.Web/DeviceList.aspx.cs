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
        public static string getDeviceList(string conditions, int rows, int page, string sort, string sortOrder)
        {
            if (rows == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            DeviceQueryBLL bll = new DeviceQueryBLL();

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = page;
            pagerInfo.PageSize = rows;

            SortInfo sortInfo = new SortInfo(sort, sortOrder);
            QueryConditionModel conditionModel = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
            if (conditionModel.QueryRange=="1")
            {
                conditionModel.DeviceState = MachineState.已连接仪器;
            }
            else if (conditionModel.QueryRange=="2")
            {
                conditionModel.DeviceState = MachineState.未连接仪器;
            }

            List<QueryList> list = new List<QueryList>();
            list = bll.GetDeviceInfoBLL(conditionModel, pagerInfo, sortInfo);
            pagerInfo.RecordCount = bll.getDeviceCount(conditionModel);

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            var reslut= JsonConvert.SerializeObject(result).Replace("null", "\"\"");

            return reslut;
        }

        [WebMethod]
        public static string BindDetial(string sn)
        {
            try
            {
                DeviceQueryBLL bll = new DeviceQueryBLL();
                var result = bll.GetDeviceDetialBLL(sn);

                var retvalue= JsonConvert.SerializeObject(result);
                return retvalue;
            }
            catch (Exception ex)
            {
            }
            return sn;
        }
    }
}