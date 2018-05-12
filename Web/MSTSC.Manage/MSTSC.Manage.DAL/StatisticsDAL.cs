﻿using Dapper;
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
    public class StatisticsDAL
    {
        /// <summary>
        /// 统计所有仪器
        /// </summary>
        /// <param name="conditValue"></param>
        /// <param name="pagerInfo"></param>
        /// <param name="sortInfo"></param>
        /// <returns></returns>
        public DataTable StatisticsAllDevicesDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            var sql = @"SELECT d.DeviceName, d.SIM, d.SN, c.count_times_total, r.reagent_dil, r.reagent_lh, r.reagent_r2 FROM device_info d INNER JOIN ( SELECT SN FROM device_info {0} ";
            sql += " LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize;
            sql += ") AS t USING (SN) LEFT JOIN blood_count c ON d.SN = c.device_sn LEFT JOIN blood_reagent r ON d.SN = r.device_sn;";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
                //return conn.Query<dynamic>(SqlCondit).ToList();
            }
        }

        public int getDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT count(1) FROM device_info d ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND d.ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND d.ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                sql += " WHERE " + whereSql.ToString().Substring(4);
            }

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(sql);
            }
        }

        public DataTable StatisticsByModelBLL(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            var sql = @"SELECT SUM(count_times_wb_cbc) AS count_times_wb_cbc,SUM(count_times_wb_cbc_crp) AS count_times_wb_cbc_crp,SUM(count_times_wb_crp) AS count_times_wb_crp,
SUM(count_times_pd_cbc) AS count_times_pd_cbc,SUM(count_times_pd_cbc_crp) AS count_times_pd_cbc_crp,SUM(count_times_pd_crp) AS count_times_pd_crp
FROM device_info,blood_count WHERE SN = device_sn {0}";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND ProductModel='" + conditValue.ModelType + "'");
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
                //return conn.Query<dynamic>(SqlCondit).ToList();
            }
        }

        /// <summary>
        /// 按封闭试剂类型统计
        /// </summary>
        /// <param name="conditValue"></param>
        /// <returns></returns>
        public DataTable StatisticsByReagentTypeDAL(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.ReagentType, COUNT(d.SN) device_count, SUM(bc.count_times_total) AS count_times_total, SUM(br.reagent_dil) AS reagent_dil, SUM(br.reagent_lh) AS reagent_lh, SUM(br.reagent_r2) AS reagent_r2 
                FROM device_info d LEFT JOIN blood_count bc ON d.SN = bc.device_sn LEFT JOIN blood_reagent br ON d.SN = br.device_sn 
                WHERE reagenttype != '' AND reagenttype IS NOT NULL {0} 
                GROUP BY ReagentType ORDER BY ReagentType DESC;";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND ProductModel='" + conditValue.ModelType + "'");
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds.Tables[0];

                //return conn.Query<dynamic>(SqlCondit).ToList();
            }
        }

        /// <summary>
        /// 统计-按区域
        /// </summary>
        /// <param name="conditValue"></param>
        /// <returns></returns>
        public DataTable StatisticsByAreaDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.Region, COUNT(d.SN) device_count, SUM(bc.count_times_total) AS count_times_total, SUM(br.reagent_dil) AS reagent_dil, SUM(br.reagent_lh) AS reagent_lh, SUM(br.reagent_r2) AS reagent_r2 
                FROM device_info d LEFT JOIN blood_count bc ON d.SN = bc.device_sn LEFT JOIN blood_reagent br ON d.SN = br.device_sn {0} 
                GROUP BY Region ORDER BY SN LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds.Tables[0];

                //return conn.Query<dynamic>(SqlCondit).ToList();
            }
        }

        public int getAreaDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT Region FROM device_info d {0} GROUP BY Region)t; ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND d.ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND d.ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(SqlCondit);
            }
        }

        /// <summary>
        /// 统计-按机型
        /// </summary>
        /// <param name="conditValue"></param>
        /// <returns></returns>
        public DataTable StatisticsByTypeDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.Model, COUNT(d.SN) device_count, SUM(bc.count_times_total) AS count_times_total, SUM(br.reagent_dil) AS reagent_dil, SUM(br.reagent_lh) AS reagent_lh, SUM(br.reagent_r2) AS reagent_r2 
                FROM device_info d LEFT JOIN blood_count bc ON d.SN = bc.device_sn LEFT JOIN blood_reagent br ON d.SN = br.device_sn {0} 
                GROUP BY Model ORDER BY SN LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds.Tables[0];

                //return conn.Query<dynamic>(SqlCondit).ToList();
            }
        }

        public int getTypeDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT Model FROM device_info d {0} GROUP BY Model)t; ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND d.ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND d.ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(SqlCondit);
            }
        }

        /// <summary>
        /// 统计-按机型
        /// </summary>
        /// <param name="conditValue"></param>
        /// <returns></returns>
        public DataTable StatisticsByOEMDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.OEM, COUNT(d.SN) device_count, SUM(bc.count_times_total) AS count_times_total, SUM(br.reagent_dil) AS reagent_dil, SUM(br.reagent_lh) AS reagent_lh, SUM(br.reagent_r2) AS reagent_r2 
                FROM device_info d LEFT JOIN blood_count bc ON d.SN = bc.device_sn LEFT JOIN blood_reagent br ON d.SN = br.device_sn {0} 
                GROUP BY OEM ORDER BY SN LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds.Tables[0];

                //return conn.Query<dynamic>(SqlCondit).ToList();
            }
        }

        public int getOEMDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT OEM FROM device_info d {0} GROUP BY OEM)t; ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND d.ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND d.ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(SqlCondit);
            }
        }

    }
}
