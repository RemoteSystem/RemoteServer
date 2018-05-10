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
    public partial class DeviceQuery : System.Web.UI.Page
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
            if (conditionModel.QueryRange == "1")
            {
                conditionModel.DeviceState = MachineState.已连接仪器;
            }
            else if (conditionModel.QueryRange == "2")
            {
                conditionModel.DeviceState = MachineState.未连接仪器;
            }

            List<QueryList> list = new List<QueryList>();
            list = bll.GetDeviceInfoBLL(conditionModel, pagerInfo, sortInfo);
            pagerInfo.RecordCount = bll.getDeviceCount(conditionModel);

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return JsonConvert.SerializeObject(result).Replace("null", "\"\"");
        }


        [WebMethod]
        public static string BindDetial(string sn)
        {
            try
            {
                DeviceQueryBLL bll = new DeviceQueryBLL();
                var result = bll.GetDeviceDetialBLL(sn);

                var retvalue = JsonConvert.SerializeObject(result);
                return retvalue;
            }
            catch (Exception ex)
            {
            }
            return sn;
        }

        /// <summary>
        /// 获取产品类型(DeviceType字段)
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string getProductType()
        {
            DeviceQueryBLL bll = new DeviceQueryBLL();
            var result = bll.getProductType();

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

        /// <summary>
        /// 获取产品系列[3diff、5diff](ProductSeries字段)
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string getProductSeries()
        {
            DeviceQueryBLL bll = new DeviceQueryBLL();
            var result = bll.getProductSeries();

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

        /// <summary>
        /// 获取产品型号[Z3、Z30、Z31、Z3CRP、Z30CRP、Z31CRP](Model字段)
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string getModel()
        {
            DeviceQueryBLL bll = new DeviceQueryBLL();
            var result = bll.getModel();

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

        /// <summary>
        /// 获取产品项目[BK、VK](ProductModel字段)
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string getProductModel()
        {
            DeviceQueryBLL bll = new DeviceQueryBLL();
            var result = bll.getProductModel();

            var retvalue = JsonConvert.SerializeObject(result);
            return retvalue;
        }

    }
}