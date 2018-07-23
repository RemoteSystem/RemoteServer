using MSTSC.Manage.BLL;
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

            DeviceQueryBLL bll = new DeviceQueryBLL();
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
                        ls_item += row[i].ToString() + "\n";
                    }
                    else
                    {
                        ls_item += row[i].ToString() + "\t";
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