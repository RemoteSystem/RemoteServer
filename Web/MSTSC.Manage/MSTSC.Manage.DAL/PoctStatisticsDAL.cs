using Dapper;
using MSTSC.Manage.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.DAL
{
    /// <summary>
    /// 报表统计
    /// </summary>
    public class PoctStatisticsDAL
    {
        public DataTable StatisticsAllPoctDevicesDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            //            string sql = @"SELECT CONCAT(IFNULL(d.Hospital,''),'_',IFNULL(d.Model,'')) as DeviceName,d.dtupdate,d.SIM,d.SN,SUM(c.smpl) AS smpl, SUM(c.card_consume) AS card_consume
            //	                FROM device_info d LEFT OUTER JOIN poct_statistics_item c
            //		            ON d.SN = c.device_sn WHERE d.DeviceType='POCT' {0} GROUP BY SN
            //	                LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize;

            //            if (!string.IsNullOrEmpty(conditValue.SN))
            //            {
            string sql = @"SELECT CONCAT(IFNULL(d.Hospital,''),'_',IFNULL(d.Model,'')) as DeviceName,c.dtinsert as dtupdate,d.SIM,d.SN,c.smpl, c.card_consume
	                FROM device_info d LEFT OUTER JOIN poct_statistics_item c
		            ON d.SN = c.device_sn WHERE d.DeviceType='POCT' {0} ORDER BY c.dtinsert DESC
	                LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize;
            //}

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.SN))
            {
                whereSql.Append(@" AND d.SN = '" + conditValue.SN + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND d.SN in(SELECT device_sn FROM poct_item WHERE card_name = '" + conditValue.Card + "')");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND c.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND c.dtinsert<='" + conditValue.dtEnd + "'");
            }

            string SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        public int getPoctDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            //            string sql = @"SELECT count(1) FROM (SELECT d.DeviceName,d.SIM,d.SN,SUM(c.smpl) AS smpl, SUM(c.card_consume) AS card_consume
            //	                FROM device_info d LEFT OUTER JOIN poct_statistics_item c
            //		            ON d.SN = c.device_sn WHERE d.DeviceType='POCT' {0} GROUP BY SN)t ";

            //            if (!string.IsNullOrEmpty(conditValue.SN))
            //            {
            string sql = @"SELECT count(1) FROM (SELECT c.dtinsert,d.SN FROM device_info d LEFT OUTER JOIN poct_statistics_item c
		            ON d.SN = c.device_sn WHERE d.DeviceType='POCT' {0} )t ";
            //}

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.SN))
            {
                whereSql.Append(@" AND d.SN = '" + conditValue.SN + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND d.SN in(SELECT device_sn FROM poct_item WHERE card_name = '" + conditValue.Card + "')");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND c.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND c.dtinsert<='" + conditValue.dtEnd + "'");
            }

            string SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(SqlCondit);
            }
        }


        public DataTable PoctStatisticsByAreaDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.Region, COUNT(d.SN) device_count, SUM(bc.smpl) AS smpl, SUM(bc.card_consume) AS card_consume
                FROM device_info d LEFT JOIN poct_statistics_item bc ON d.SN = bc.device_sn where DeviceType ='POCT' {0} 
                GROUP BY Region ORDER BY SN LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND d.Region = '" + conditValue.Region + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND d.SN in(SELECT device_sn FROM poct_item WHERE card_name = '" + conditValue.Card + "')");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND bc.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND bc.dtinsert<='" + conditValue.dtEnd + "'");
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds.Tables[0];
            }
        }

        public int getAreaPoctDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT d.Region FROM device_info d 
                           LEFT JOIN poct_statistics_item bc ON d.SN = bc.device_sn  WHERE d.DeviceType ='POCT' {0} GROUP BY Region)t; ";

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND d.Region = '" + conditValue.Region + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND d.SN in(SELECT device_sn FROM poct_item WHERE card_name = '" + conditValue.Card + "')");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND bc.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND bc.dtinsert<='" + conditValue.dtEnd + "'");
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(SqlCondit);
            }
        }

        public DataTable PoctStatisticsByTypeDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT case d.MachineType when 0 then '标准机' when 1 then '招标机' else '其他' end AS MachineType, 
                COUNT(d.SN) device_count, SUM(bc.smpl) AS smpl, SUM(bc.card_consume) AS card_consume 
                FROM device_info d LEFT JOIN poct_statistics_item bc ON d.SN = bc.device_sn WHERE DeviceType ='POCT' {0} 
                GROUP BY MachineType ORDER BY SN LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries) && conditValue.ProductSeries == "1")//标准机
            {
                whereSql.Append(@" AND d.MachineType=0");
            }
            else if (!string.IsNullOrEmpty(conditValue.ProductSeries) && conditValue.ProductSeries == "2")//招标机
            {
                whereSql.Append(@" AND d.MachineType=1");
            }
            else if (!string.IsNullOrEmpty(conditValue.ProductSeries) && conditValue.ProductSeries == "3")//其他
            {
                whereSql.Append(@" AND d.MachineType<>0 AND d.MachineType<>1");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND d.SN in(SELECT device_sn FROM poct_item WHERE card_name = '" + conditValue.Card + "')");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND bc.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND bc.dtinsert<='" + conditValue.dtEnd + "'");
            }


            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds.Tables[0];
            }
        }

        public int getTypePoctDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT d.MachineType FROM device_info d 
                           LEFT JOIN poct_statistics_item bc ON d.SN = bc.device_sn WHERE DeviceType ='POCT' {0} GROUP BY MachineType)t; ";

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries) && conditValue.ProductSeries == "1")//标准机
            {
                whereSql.Append(@" AND d.MachineType=0");
            }
            else if (!string.IsNullOrEmpty(conditValue.ProductSeries) && conditValue.ProductSeries == "2")//招标机
            {
                whereSql.Append(@" AND d.MachineType=1");
            }
            else if (!string.IsNullOrEmpty(conditValue.ProductSeries) && conditValue.ProductSeries == "3")//其他
            {
                whereSql.Append(@" AND d.MachineType<>0 AND d.MachineType<>1");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND d.SN in(SELECT device_sn FROM poct_item WHERE card_name = '" + conditValue.Card + "')");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND bc.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND bc.dtinsert<='" + conditValue.dtEnd + "'");
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(SqlCondit);
            }
        }

        public DataTable StatisticsAllPoctDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT t.Model,t.card_name,SUM(c.smpl) AS smpl, SUM(c.card_consume) AS card_consume FROM (SELECT DISTINCT d.SN,d.Model,p.card_name FROM device_info d,poct_item p WHERE d.DeviceType='POCT' 
                    AND d.SN = p.device_sn {0} )t LEFT OUTER JOIN poct_statistics_item c ON t.SN = c.device_sn group by t.Model,t.card_name ORDER BY t.Model
	                LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize; ;

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND p.card_name = '" + conditValue.Card + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND p.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND p.dtinsert<='" + conditValue.dtEnd + "'");
            }

            string SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        public int getPoctAllCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT count(cn) FROM(SELECT count(1)cn FROM (SELECT DISTINCT d.SN,d.Model,p.card_name FROM device_info d,poct_item p WHERE d.DeviceType='POCT' 
                          AND d.SN = p.device_sn {0} )t LEFT OUTER JOIN poct_statistics_item c ON t.SN = c.device_sn GROUP BY t.Model,t.card_name)m ";

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.Card))
            {
                whereSql.Append(@" AND p.card_name = '" + conditValue.Card + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND p.dtinsert>='" + conditValue.dtStart + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND p.dtinsert<='" + conditValue.dtEnd + "'");
            }

            string SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(SqlCondit);
            }
        }

    }
}
