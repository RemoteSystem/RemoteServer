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
    public partial class StatisticsByReagentType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string getDeviceList(string conditions)
        {
            StatisticsBLL bll = new StatisticsBLL();
            QueryConditionModel conditionModel = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
            var dt = bll.StatisticsByReagentTypeBLL(conditionModel);
            var deivceTotal = Convert.ToInt32(dt.Compute("sum(机器数)", "true"));
            var countTimesTotal = Convert.ToInt32(dt.Compute("sum(样本数)", "true"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (deivceTotal > 0)
                {
                    dt.Rows[i]["机器数比例"] = Math.Round((double)(Convert.ToInt32(dt.Rows[i]["机器数"]) / deivceTotal * 100), 2) + "%";
                }
                
                if(countTimesTotal>0)
                {
                    dt.Rows[i]["样本数比例"] = Math.Round((double)(Convert.ToInt32(dt.Rows[i]["样本数"]) / deivceTotal * 100), 2) + "%";
                }
            }

            return JsonConvert.SerializeObject(dt).Replace("null", "\"\"");
        }
    }
}