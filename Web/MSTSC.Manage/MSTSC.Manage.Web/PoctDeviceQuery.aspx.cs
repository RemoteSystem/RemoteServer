using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using MSTSC.Manage.Utils;
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
    public partial class PoctDeviceQuery : BasePage
    {
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
        public static string getDeviceSampleList(string conditions)
        {
            if (string.IsNullOrEmpty(conditions))
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            DeviceQueryBLL bll = new DeviceQueryBLL();

            List<PoctStatistics> list = new List<PoctStatistics>();
            list = bll.GetPoctDeviceInfoBLL(conditions);

            var result = new { total = list.Count, rows = list };
            return JsonConvert.SerializeObject(result).Replace("null", "\"\"");
        }

        /// <summary>
        /// 获取产品型号(Model字段)
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string getModel()
        {
            DeviceQueryBLL bll = new DeviceQueryBLL();
            var result = bll.GetPoctModel();

            var retvalue = JsonConvert.SerializeObject(result).Replace("null", "\"\"");
            return retvalue;
        }

        /// <summary>
        /// 获取装机区域
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string getRegion()
        {
            //DeviceQueryBLL bll = new DeviceQueryBLL();
            //var result = bll.GetPoctRegion();

            List<KeyValueModel> result = CommonUtils.getAllRegions();

            var retvalue = JsonConvert.SerializeObject(result).Replace("null", "\"\""); ;
            return retvalue;
        }

        [WebMethod]
        public static string getPoctDeviceDetial(string sn)
        {
            try
            {
                DeviceQueryBLL bll = new DeviceQueryBLL();
                var result = bll.GetPoctDeviceDetialBLL(sn);

                var retvalue = JsonConvert.SerializeObject(result).Replace("null", "\"\""); ;
                return retvalue;
            }
            catch (Exception ex)
            {
            }
            return sn;
        }

        [WebMethod]
        public static string getPoctDeviceFault(string sn, string dtstart, string dtend)
        {
            try
            {
                DeviceQueryBLL bll = new DeviceQueryBLL();
                var result = bll.GetPoctDeviceFaultBLL(sn, dtstart, dtend);

                var retvalue = JsonConvert.SerializeObject(result).Replace("null", "\"\""); ;
                return retvalue;
            }
            catch (Exception ex)
            {
            }
            return sn;
        }

        [WebMethod]
        public static string getNumDetail(string sn, string num, string lot)
        {
            try
            {
                DeviceQueryBLL bll = new DeviceQueryBLL();
                var result = bll.GetPoctNumDetail(sn, num, lot);

                var retvalue = JsonConvert.SerializeObject(result).Replace("null", "\"\""); ;
                return retvalue;
            }
            catch (Exception ex)
            {
            }
            return "{}";
        }

        [WebMethod]
        public static string getNumLotList(string sn, string num)
        {
            try
            {
                DeviceQueryBLL bll = new DeviceQueryBLL();
                var result = bll.getNumLotList(sn, num);

                var retvalue = JsonConvert.SerializeObject(result).Replace("null", "\"\""); ;
                return retvalue;
            }
            catch (Exception ex)
            {
            }
            return "{}";
        }

    }
}