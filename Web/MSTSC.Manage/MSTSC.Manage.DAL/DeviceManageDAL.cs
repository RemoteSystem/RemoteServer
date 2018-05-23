using Dapper;
using MSTSC.Manage.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.DAL
{
    public class DeviceManageDAL
    {
        public List<QueryList> GetDeviceInfoDAL(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.DeviceName, d.SIM, d.SN,d.SESSIONID,d.ProductSeries, d.ProductModel
                            FROM device_info d
                                where 1=1 ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND d.DeviceType='" + conditValue.DeviceType + "'");
            }

            if (conditValue.DeviceState == MachineState.已连接仪器)
            {
                whereSql.Append(@" AND d.sessionid is not null");
            }
            else if (conditValue.DeviceState == MachineState.未连接仪器)
            {
                whereSql.Append(@" AND d.sessionid is null");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND d.ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND d.ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.OEM))
            {
                whereSql.Append(@" AND d.OEM='" + conditValue.OEM + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Agent))
            {
                whereSql.Append(@" AND d.Agent='" + conditValue.Agent + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ReagentType))
            {
                whereSql.Append(@" AND d.ReagentType='" + conditValue.ReagentType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND d.Region='" + conditValue.Region + "'");
            }

            sql = sql + whereSql.ToString();

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<QueryList>(sql).ToList();
            }
        }

        public List<QueryList> QuickQueryDAL(string queryText)
        {
            string sql = @"SELECT d.DeviceName,d.Region, d.SIM, d.SN,d.ProductSeries,d.ProductModel, d.SESSIONID
                              FROM device_info d
                            where d.SN='" + queryText +
                            "' or d.devicename='" + queryText +
                            "' or d.SIM='" + queryText + "'";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<QueryList>(sql).ToList();
            }
        }


        public dynamic GetDeviceDetialDAL(string sn)
        {
            string sql = @"SELECT d.devicename,d.SIM,d.SN,d.DeviceType,d.Model,d.OEM,d.Agent,d.Region,
case  when reagenttype='open' then '开放' when reagenttype='close' then '封闭' else reagenttype end ReagentType ,
d.InstallationArea,if(d.FactoryDate='0001-01-01','',d.FactoryDate) as FactoryDate,if(d.InstallDate='0001-01-01','',d.InstallDate) as InstallDate,
date_format(d.UpdateTime,'%Y-%m-%d %T') as UpdateTime,brt.runtime_days,brt.runtime_opt,brt.runtime_power,brt.runtime_air_supply,
bm.needle_times_impale,bc.count_times_total,bc.count_times_wb_cbc,bc.count_times_wb_cbc_crp,bc.count_times_wb_crp,bc.count_times_pd_cbc,
bc.count_times_pd_cbc_crp,bc.count_times_pd_crp,bc.count_times_wb_cd,bc.count_times_wb_cd_crp,bc.count_times_pd_cd,bc.count_times_pd_cd_crp,bc.count_times_qc,
br.reagent_dil,br.reagent_lh,br.reagent_r2,br.reagent_diff1,br.reagent_diff2,
br.reagent_r1,br.reagent_fl1,br.reagent_fl2,br.reagent_fl3,br.reagent_fl4,br.reagent_fl5,br.reagent_fl6,bm.hole_times_wbc,bm.hole_times_rbc,
bm.sampling_times_fault,bm.syringe_times_syringe_fault,bm.inject_times_fault,bm.mixing_times_fault
                        from device_info d
                             LEFT JOIN blood_runtime brt on d.SN=brt.device_sn 
                             left JOIN blood_count bc on d.SN=bc.device_sn
                             LEFT JOIN blood_reagent br on d.SN = br.device_sn
                             LEFT JOIN blood_module bm on d.SN=bm.device_sn
                            where d.SN='" + sn + "'";
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query(sql);
            }
        }

        public List<ProductTypeModel> ProductTypeInfoDAL()
        {
            var sql = @"SELECT DISTINCT DeviceType,ProductSeries,ProductModel from device_info ";
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<ProductTypeModel>(sql).ToList();
            }
        }

        //******************* NEW *****************//
        public List<QueryList> GetDeviceInfoDAL(QueryConditionModel conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.DeviceName, d.SIM, d.SN,d.SESSIONID,d.ProductSeries,d.Model FROM device_info d INNER JOIN (SELECT SN FROM device_info ";

            if (!string.IsNullOrEmpty(conditValue.DeviceType))
            {
                whereSql.Append(@" AND DeviceType='" + conditValue.DeviceType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.QueryText))
            {
                whereSql.Append(@" AND (SN='" + conditValue.QueryText +
                            "' or devicename='" + conditValue.QueryText +
                            "' or SIM='" + conditValue.QueryText + "')");
            }

            if (conditValue.DeviceState == MachineState.已连接仪器)
            {
                whereSql.Append(@" AND sessionid is not null");
            }
            else if (conditValue.DeviceState == MachineState.未连接仪器)
            {
                whereSql.Append(@" AND sessionid is null");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.OEM))
            {
                whereSql.Append(@" AND OEM='" + conditValue.OEM + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Agent))
            {
                whereSql.Append(@" AND Agent='" + conditValue.Agent + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ReagentType))
            {
                whereSql.Append(@" AND ReagentType='" + conditValue.ReagentType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND Region='" + conditValue.Region + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                sql += " where "+ whereSql.ToString().Substring(4);
            }
            if (!string.IsNullOrEmpty(sortInfo.SortName))
            {
                sql += " ORDER BY " + sortInfo.SortName + " " + sortInfo.SortOrder;
            }
            sql += " LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize;

            sql += " ) AS t USING(SN);";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<QueryList>(sql).ToList();
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

            if (!string.IsNullOrEmpty(conditValue.QueryText))
            {
                whereSql.Append(@" AND (SN='" + conditValue.QueryText +
                            "' or devicename='" + conditValue.QueryText +
                            "' or SIM='" + conditValue.QueryText + "')");
            }

            if (conditValue.DeviceState == MachineState.已连接仪器)
            {
                whereSql.Append(@" AND d.sessionid is not null");
            }
            else if (conditValue.DeviceState == MachineState.未连接仪器)
            {
                whereSql.Append(@" AND d.sessionid is null");
            }

            if (!string.IsNullOrEmpty(conditValue.ProductSeries))
            {
                whereSql.Append(@" AND d.ProductSeries='" + conditValue.ProductSeries + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND d.ProductModel='" + conditValue.ModelType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.OEM))
            {
                whereSql.Append(@" AND d.OEM='" + conditValue.OEM + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Agent))
            {
                whereSql.Append(@" AND d.Agent='" + conditValue.Agent + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.ReagentType))
            {
                whereSql.Append(@" AND d.ReagentType='" + conditValue.ReagentType + "'");
            }

            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND d.Region='" + conditValue.Region + "'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                sql += " where " + whereSql.ToString().Substring(4);
            }

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(sql);
            }
        }


        /// <summary>
        /// 获取产品类型(DeviceType字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getProductType()
        {
            string sql = "SELECT DISTINCT DeviceType 'key',DeviceType 'value' FROM device_info;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取产品系列[3diff、5diff](ProductSeries字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getProductSeries()
        {
            string sql = "SELECT DISTINCT ProductSeries 'key',ProductSeries 'value' FROM device_info;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取产品型号[Z3、Z30、Z31、Z3CRP、Z30CRP、Z31CRP](Model字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getModel()
        {
            string sql = "SELECT DISTINCT Model 'key',Model 'value' FROM device_info;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取产品项目[BK、VK](ProductModel字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getProductModel()
        {
            string sql = "SELECT DISTINCT ProductModel 'key',ProductModel 'value' FROM device_info;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }
       
    }
}
