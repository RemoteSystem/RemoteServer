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
    public partial class StatisticByClose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string getDataList(string conditions, int rows)
        {
            if (rows == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }

            StatisticsBLL bll = new StatisticsBLL();
            QueryConditionModel conditionModel = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));

            DataTable dt = bll.StatisticsByReagentTypeBLL(conditionModel);

            if (dt.Rows.Count < 2)
            {
                DataRow dr;
                if (dt.Rows.Count == 0)
                {
                    dr = dt.NewRow();
                    dr[0] = "open";
                    dr[1] = dr[2] = dr[3] = dr[4] = dr[5] = 0;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr[0] = "close";
                    dr[1] = dr[2] = dr[3] = dr[4] = dr[5] = 0;
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dt.Rows[0][0].ToString().ToLower() == "close")
                    {
                        dr = dt.NewRow();
                        dr[0] = "open";
                        dr[1] = dr[2] = dr[3] = dr[4] = dr[5] = 0;
                        dt.Rows.InsertAt(dr, 0);
                    }
                    else if (dt.Rows[0][0].ToString().ToLower() == "open")
                    {
                        dr = dt.NewRow();
                        dr[0] = "close";
                        dr[1] = dr[2] = dr[3] = dr[4] = dr[5] = 0;
                        dt.Rows.Add(dr);
                    }
                }
            }

            //构造成Json的格式传递
            return JsonConvert.SerializeObject(dt).Replace("null", "0");
        }
    }
}