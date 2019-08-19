using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MSTSC.Manage.Web
{
    /// <summary>
    /// Export 的摘要说明
    /// </summary>
    public class Export : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string message = "";
            string Action = context.Request["Action"];
            //params
            string sn = context.Request["sn"] ?? "";
            string model = context.Request["model"] ?? "";
            if (model.Trim() == "0") model = "";

            string conditions = context.Request["conditions"] ?? "";

            DeviceQueryBLL bll = new DeviceQueryBLL();
            StatisticsBLL staticBll = new StatisticsBLL();
            PoctStatisticsBLL poctStaticBll = new PoctStatisticsBLL();
            QueryConditionModel condition;
            DataTable dt = new DataTable();
            string fileName = "";
            string[] headers = new string[] { };
            switch (Action)
            {
                case "fault":
                    dt = bll.GetDeviceFaultForExportBLL(sn);//获取导出数据源  
                    fileName = "错误信息";
                    headers = new string[] { "错误码", "时间" };
                    break;
                case "yangben":
                    dt = bll.GetDeviceSampleForExportBLL();//获取导出数据源  
                    fileName = "样本信息";
                    headers = new string[] { "system_seq", "仪器序列号", "计数值", "计数类型", "时间" };
                    break;
                case "cuowu":
                    dt = bll.GetDeviceFaultForExportBLL();//获取导出数据源  
                    fileName = "错误信息";
                    headers = new string[] { "id", "仪器序列号", "错误码", "时间" };
                    break;
                case "bio_fault":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = bll.GetBioDeviceFaultBLL(condition.SN, condition.dtStart, condition.dtEnd);
                    fileName = "生化仪统计_故障信息";
                    headers = new string[] { "错误码", "时间" };
                    break;
                case "bio_all":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = staticBll.StatisticsAllBioDevicesForExportBLL(condition);
                    fileName = "生化仪统计_所有机器";
                    headers = new string[] { "仪器名", "SIM卡号", "仪器序列号", "仪器型号", "项目编号", "样本数", "R1消耗量", "R2消耗量" };
                    break;
                case "bio_area":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = staticBll.BioStatisticsByAreaForExportBLL(condition);
                    fileName = "生化仪统计_按区域";
                    headers = new string[] { "装机区域", "项目编号", "仪器总数", "样本数", "R1消耗量", "R2消耗量" };
                    break;
                case "bio_type":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = staticBll.BioStatisticsByTypeForExportBLL(condition);
                    fileName = "生化仪统计_按机型";
                    headers = new string[] { "机型", "项目编号", "仪器总数", "样本数", "R1消耗量", "R2消耗量" };
                    break;
                case "poct_fault":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = bll.GetPoctDeviceFaultBLL(condition.SN, condition.dtStart, condition.dtEnd);
                    fileName = "POCT统计_故障信息";
                    headers = new string[] { "错误码", "时间" };
                    break;
                case "poct_all":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = poctStaticBll.StatisticsAllPoctDevicesForExportBLL(condition);
                    fileName = "POCT统计_所有机器";
                    headers = new string[] { "机器名", "SIM卡号", "仪器序列号", "仪器型号", "项目编号", "样本数", "测试卡消耗数" };
                    break;
                case "poct_area":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = poctStaticBll.PoctStatisticsByAreaForExportBLL(condition);
                    fileName = "POCT统计_按区域";
                    headers = new string[] { "装机区域", "项目编号", "仪器总数", "样本数", "测试卡消耗数" };
                    break;
                case "poct_type":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = poctStaticBll.PoctStatisticsByTypeForExportBLL(condition);
                    fileName = "POCT统计_按机型";
                    headers = new string[] { "机型", "项目编号", "仪器总数", "样本数", "测试卡消耗数" };
                    break;
                case "poct_all_num":
                    condition = JsonConvert.DeserializeObject<QueryConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = poctStaticBll.StatisticsAllPoctNumForExportBLL(condition);
                    fileName = "POCT统计_总量统计";
                    headers = new string[] { "仪器型号", "测试项目", "项目编号", "样本数", "测试卡消耗数" };
                    break;
                case "bio_log":
                    LogConditionModel conditionModel = JsonConvert.DeserializeObject<LogConditionModel>(conditions.Replace("\"0\"", "\"\""));
                    dt = staticBll.StatisticsLogsForExportBLL(conditionModel);
                    fileName = "日志查询";
                    headers = new string[] { "仪器名称", "SIM卡号", "仪器序列号", "仪器类型", "仪器型号", "发生时间", "日志内容" };
                    break;
            }
            ExportExcel(context, dt, headers, fileName + sn + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            context.Response.Write(message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// DataTable导出到Excel
        /// </summary>
        /// <param name="context">httpcontex</param>
        /// <param name="dt">DataTable类型的数据源</param>
        /// <param name="headers">表头</param>
        /// <param name="FileName">文件名</param>
        public void ExportExcel(HttpContext context, DataTable dt, string[] headers, string FileName)
        {
            if (dt.Rows.Count <= 0) return;

            context.Response.Clear();
            context.Response.Charset = "UTF-8";
            context.Response.Buffer = true;
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            context.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls\"");
            context.Response.ContentType = "application/ms-excel";
            string colHeaders = string.Empty;
            string ls_item = string.Empty;
            DataRow[] myRow = dt.Select();
            int i = 0;
            int cl = dt.Columns.Count;
            for (int j = 0; j < headers.Length; j++)
            {
                ls_item += headers[j] + "\t"; //栏位：自动跳到下一单元格  
            }
            ls_item = ls_item.Substring(0, ls_item.Length - 1) + "\n";
            foreach (DataRow row in myRow)
            {
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))
                    {
                        ls_item += row[i].ToString().Replace("\n","").Replace("\t","") + "\n";
                    }
                    else
                    {
                        ls_item += "=\"" + row[i].ToString() + "\"\t";
                    }
                }
                context.Response.Output.Write(ls_item);
                ls_item = string.Empty;
            }
            context.Response.Output.Flush();
            context.Response.End();
        }

    }
}