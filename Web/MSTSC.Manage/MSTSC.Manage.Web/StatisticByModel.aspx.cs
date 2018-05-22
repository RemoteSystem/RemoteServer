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
    public partial class StatisticByModel : BasePage
    {
        [WebMethod]
        public static string getDataList(string conditions, int rows)
        {
            if (rows == 0)
            {
                return "[]";
            }

            StatisticsBLL bll = new StatisticsBLL();
            QueryConditionModel conditionModel = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));

            DataTable dt = bll.StatisticsByModelBLL(conditionModel);
            List<KeyValueModel> list = new List<KeyValueModel>();
            if (dt.Rows.Count > 0)
            {
                list.Add(new KeyValueModel("全血-CBC", string.IsNullOrEmpty(dt.Rows[0]["count_times_wb_cbc"].ToString()) ? "0" : dt.Rows[0]["count_times_wb_cbc"].ToString()));
                list.Add(new KeyValueModel("全血-CBC+CRP", string.IsNullOrEmpty(dt.Rows[0]["count_times_wb_cbc_crp"].ToString()) ? "0" : dt.Rows[0]["count_times_wb_cbc_crp"].ToString()));
                list.Add(new KeyValueModel("全血-CRP", string.IsNullOrEmpty(dt.Rows[0]["count_times_wb_crp"].ToString()) ? "0" : dt.Rows[0]["count_times_wb_crp"].ToString()));
                list.Add(new KeyValueModel("预稀释-CBC", string.IsNullOrEmpty(dt.Rows[0]["count_times_pd_cbc"].ToString()) ? "0" : dt.Rows[0]["count_times_pd_cbc"].ToString()));
                list.Add(new KeyValueModel("预稀释-CBC+CRP", string.IsNullOrEmpty(dt.Rows[0]["count_times_pd_cbc_crp"].ToString()) ? "0" : dt.Rows[0]["count_times_pd_cbc_crp"].ToString()));
                list.Add(new KeyValueModel("预稀释-CRP", string.IsNullOrEmpty(dt.Rows[0]["count_times_pd_crp"].ToString()) ? "0" : dt.Rows[0]["count_times_pd_crp"].ToString()));
            }
            else
            {
                list.Add(new KeyValueModel("全血-CBC", "0"));
                list.Add(new KeyValueModel("全血-CBC+CRP", "0"));
                list.Add(new KeyValueModel("全血-CRP", "0"));
                list.Add(new KeyValueModel("预稀释-CBC", "0"));
                list.Add(new KeyValueModel("预稀释-CBC+CRP", "0"));
                list.Add(new KeyValueModel("预稀释-CRP", "0"));
            }

            //构造成Json的格式传递
            return JsonConvert.SerializeObject(list).Replace("null", "0");
        }

    }
}