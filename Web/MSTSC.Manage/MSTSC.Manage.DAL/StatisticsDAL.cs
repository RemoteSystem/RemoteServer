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
            var sql = @"SELECT IFNULL(d.DeviceName,'') AS DeviceName, d.SIM, d.SN, c.count_times_total, r.reagent_dil, r.reagent_lh, r.reagent_r2 FROM device_info d INNER JOIN ( SELECT SN FROM device_info {0} ";
            sql += " LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize;
            sql += ") AS t USING (SN) LEFT JOIN blood_count c ON d.SN = c.device_sn LEFT JOIN blood_reagent r ON d.SN = r.device_sn;";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND DeviceType ='血液细胞分析仪'");
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
            string sql = @"SELECT count(1) FROM device_info d where ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND d.DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND d.DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND d.DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND DeviceType ='血液细胞分析仪'");
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
            else
            {
                whereSql.Append(@" AND d.DeviceType ='血液细胞分析仪'");
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

        /***********bio***********/

        public DataTable StatisticsAllBioDevicesDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT IFNULL(d.DeviceName, '') AS DeviceName,d.SIM,d.SN,d.Model,c.num,c.smpl,c.R1,c.R2
	                FROM device_info d LEFT OUTER JOIN bio_statistics_item c
		            ON d.SN = c.device_sn WHERE d.DeviceType='生化仪' {0}
	                LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize; ;

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
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

        public int getBioDeviceCount(QueryConditionModel conditValue)
        {
            string sql = @"SELECT count(1) FROM device_info d LEFT OUTER JOIN bio_statistics_item c ON d.SN = c.device_sn WHERE d.DeviceType='生化仪' ";

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                sql += @" AND d.Model='" + conditValue.Model + "'";
            }

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(sql);
            }
        }


        public DataTable BioStatisticsByAreaDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.Region,bc.num, COUNT(d.SN) device_count, SUM(bc.smpl) AS smpl, SUM(bc.R1) AS R1, SUM(bc.R2) AS R2 
                FROM device_info d LEFT JOIN bio_statistics_item bc ON d.SN = bc.device_sn {0} 
                GROUP BY Region,bc.num ORDER BY SN LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND DeviceType ='生化仪'");
            }

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND Model='" + conditValue.Model + "'");
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
            }
        }

        public int getAreaBioDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT d.Region,bc.num FROM device_info d 
                           LEFT JOIN bio_statistics_item bc ON d.SN = bc.device_sn {0} GROUP BY Region,bc.num)t; ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND d.DeviceType ='生化仪'");
            }

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
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

        public DataTable BioStatisticsByTypeDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT case d.MachineType when 0 then '标准机' when 1 then '招标机' else '其他' end AS MachineType, bc.num,
                COUNT(d.SN) device_count, SUM(bc.smpl) AS smpl, SUM(bc.R1) AS R1, SUM(bc.R2) AS R2 
                FROM device_info d LEFT JOIN bio_statistics_item bc ON d.SN = bc.device_sn {0} 
                GROUP BY MachineType,bc.num ORDER BY SN LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND DeviceType ='生化仪'");
            }

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND Model='" + conditValue.Model + "'");
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
            }
        }

        public int getTypeBioDeviceCount(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT d.MachineType, bc.num FROM device_info d 
                           LEFT JOIN bio_statistics_item bc ON d.SN = bc.device_sn {0} GROUP BY MachineType,bc.num)t; ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND d.DeviceType ='生化仪'");
            }

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND d.Model='" + conditValue.Model + "'");
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


        public DataTable StatisticsLogsDAL(LogConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.DeviceName,d.SIM,d.SN,d.DeviceType,d.Model,dl.dtinsert,dl.content
                            FROM device_log dl LEFT JOIN device_info d ON dl.device_sn = d.SN {0}
                            ORDER BY dl.dtinsert DESC, dl.id LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize + ";";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND DeviceType ='生化仪'");
            }

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND Model='" + conditValue.Model + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND Region='" + conditValue.Region + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.DeviceName))
            {
                whereSql.Append(@" AND DeviceName='" + conditValue.DeviceName + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.SIM))
            {
                whereSql.Append(@" AND SIM='" + conditValue.SIM + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.SN))
            {
                whereSql.Append(@" AND SN='" + conditValue.SN + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND dtInsert >='" + conditValue.dtStart + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND dtInsert <='" + conditValue.dtEnd + "'");
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
            }
        }

        public int getLogCount(LogConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT COUNT(1) AS count FROM(SELECT dl.device_sn FROM device_log dl LEFT JOIN device_info d ON dl.device_sn = d.SN {0})t; ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }
            else
            {
                whereSql.Append(@" AND DeviceType ='生化仪'");
            }

            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND Model='" + conditValue.Model + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND Region='" + conditValue.Region + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.DeviceName))
            {
                whereSql.Append(@" AND DeviceName='" + conditValue.DeviceName + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.SIM))
            {
                whereSql.Append(@" AND SIM='" + conditValue.SIM + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.SN))
            {
                whereSql.Append(@" AND SN='" + conditValue.SN + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.dtStart))
            {
                whereSql.Append(@" AND dtInsert >='" + conditValue.dtStart + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.dtEnd))
            {
                whereSql.Append(@" AND dtInsert <='" + conditValue.dtEnd + "'");
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
