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

        /// <summary>
        /// 获取仪器最后一次上报的错误信息
        /// </summary>
        /// <param name="sn">仪器序列号</param>
        /// <returns>仪器最后一次上报的错误信息(最多5条)</returns>
        public dynamic GetDeviceFaultDAL(string sn)
        {
            string sql = @"SELECT code,dttime FROM blood_fault WHERE dtinsert > ( SELECT DATE_ADD(dtinsert, INTERVAL -5 SECOND) FROM blood_fault WHERE device_sn = '" + sn + "' ORDER BY id DESC LIMIT 1 ) AND device_sn = '" + sn + "' ORDER BY id DESC LIMIT 5;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query(sql);
            }
        }

        /// <summary>
        /// 获取仪器上报的错误信息
        /// </summary>
        /// <param name="sn">仪器序列号</param>
        /// <returns>仪器上报的错误信息(最多1000条)</returns>
        public DataTable GetDeviceFaultForExportDAL(string sn)
        {
            string sql = @"SELECT code,dttime FROM blood_fault WHERE device_sn = '" + sn + "' ORDER BY id DESC LIMIT 1000;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
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
            string sql = @"SELECT d.DeviceName, d.SIM, d.SN,d.SESSIONID,d.ProductSeries,d.Model,d.Region FROM device_info d INNER JOIN (SELECT SN FROM device_info ";

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
            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.HosAddr))
            {
                whereSql.Append(@" AND Address like '%" + conditValue.HosAddr + "%'");
            }
            if (!string.IsNullOrEmpty(conditValue.HosName))
            {
                whereSql.Append(@" AND Hospital like '%" + conditValue.HosName + "%'");
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                sql += " where " + whereSql.ToString().Substring(4);
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
            if (!string.IsNullOrEmpty(conditValue.Model))
            {
                whereSql.Append(@" AND Model='" + conditValue.Model + "'");
            }
            if (!string.IsNullOrEmpty(conditValue.HosAddr))
            {
                whereSql.Append(@" AND Address like '%" + conditValue.HosAddr + "%'");
            }
            if (!string.IsNullOrEmpty(conditValue.HosName))
            {
                whereSql.Append(@" AND Hospital like '%" + conditValue.HosName + "%'");
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
            string sql = "SELECT DISTINCT DeviceType 'key',DeviceType 'value' FROM device_info where DeviceType ='血液细胞分析仪';";

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
            string sql = "SELECT DISTINCT ProductSeries 'key',ProductSeries 'value' FROM device_info where DeviceType ='血液细胞分析仪';";

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
            string sql = "SELECT DISTINCT Model 'key',Model 'value' FROM device_info where DeviceType ='血液细胞分析仪';";

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
            string sql = "SELECT DISTINCT ProductModel 'key',ProductModel 'value' FROM device_info where DeviceType ='血液细胞分析仪';";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取全部样本信息(导出)
        /// </summary>
        /// <returns>获取全部样本信息(导出)</returns>
        public DataTable GetDeviceSampleForExportDAL()
        {
            string sql = @"SELECT system_seq,device_sn,device_count,device_count_type,device_update_time FROM blood_count_detail;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 获取全部错误信息(导出)
        /// </summary>
        /// <returns>获取全部错误信息(导出)</returns>
        public DataTable GetDeviceFaultForExportDAL()
        {
            string sql = @"SELECT id,device_sn,code,dttime FROM blood_fault;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        /************bio**********/
        #region bio 生化仪

        public dynamic GetBioDeviceDetialDAL(string sn)
        {
            string sql = @"SELECT d.devicename,d.SIM,d.SN,d.DeviceType,d.Model,d.Region,d.Address,d.Hospital,date_format(d.UpdateTime,'%Y-%m-%d %T') as UpdateTime,bs.sample
                        from device_info d left join bio_statistics bs on bs.device_sn = d.SN where d.SN='" + sn + "'";
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query(sql);
            }
        }

        /// <summary>
        /// 获取仪器最后一次上报的错误信息
        /// </summary>
        /// <param name="sn">仪器序列号</param>
        /// <returns>仪器最后一次上报的错误信息(最多5条)</returns>
        public DataTable GetBioDeviceFaultDAL(string sn, string dtstart, string dtend)
        {
            string sql = @"SELECT code,dttime FROM bio_fault WHERE device_sn= '" + sn + "' AND dttime >= '" + dtstart + "' AND dttime <= '" + dtend + "' ORDER BY id DESC;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 获取产品型号[Z3、Z30、Z31、Z3CRP、Z30CRP、Z31CRP](Model字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getBioModel()
        {
            string sql = "SELECT DISTINCT Model 'key',Model 'value' FROM device_info where DeviceType ='生化仪';";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取装机区域
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> getRegion()
        {
            string sql = "SELECT DISTINCT Region 'key',Region 'value' FROM device_info where DeviceType ='生化仪';";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        public List<BioStatistics> GetDeviceInfoDAL(string sn)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT device_sn,num,R1,R2,smpl FROM bio_statistics_item WHERE device_sn = '" + sn + "'";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<BioStatistics>(sql).ToList();
            }
        }

        public List<BioItemDetail> getNumDetail(string sn, string num)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT * FROM bio_item WHERE num = '" + num + "' AND device_sn='" + sn + "'";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<BioItemDetail>(sql).ToList();
            }
        }

        #endregion

        /************POCT**********/
        #region POCT

        public dynamic GetPoctDeviceDetialDAL(string sn)
        {
            string sql = @"SELECT d.devicename,d.SIM,d.SN,d.DeviceType,d.Model,d.Region,d.Address,d.Hospital,date_format(d.UpdateTime,'%Y-%m-%d %T') as UpdateTime,bs.sample
                        from device_info d left join poct_statistics bs on bs.device_sn = d.SN where d.SN='" + sn + "'";
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query(sql);
            }
        }

        /// <summary>
        /// 获取仪器最后一次上报的错误信息
        /// </summary>
        /// <param name="sn">仪器序列号</param>
        /// <returns>仪器最后一次上报的错误信息(最多5条)</returns>
        public DataTable GetPoctDeviceFaultDAL(string sn, string dtstart, string dtend)
        {
            string sql = @"SELECT code,dttime FROM poct_fault WHERE device_sn= '" + sn + "' AND dttime >= '" + dtstart + "' AND dttime <= '" + dtend + "' ORDER BY id DESC;";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 获取产品型号(Model字段)
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> GetPoctModel()
        {
            string sql = "SELECT DISTINCT Model 'key',Model 'value' FROM device_info where DeviceType ='POCT';";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取装机区域
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> GetPoctRegion()
        {
            string sql = "SELECT DISTINCT Region 'key',Region 'value' FROM device_info where DeviceType ='POCT';";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        public List<PoctStatistics> GetPoctDeviceInfoDAL(string sn)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT device_sn,num,card_consume,smpl FROM poct_statistics_item WHERE device_sn = '" + sn + "'";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<PoctStatistics>(sql).ToList();
            }
        }

        public List<PoctItemDetail> GetPoctNumDetail(string sn, string num, string lot)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT * FROM poct_item WHERE num = '" + num + "' AND device_sn='" + sn + "'";
            if (!String.IsNullOrEmpty(lot))
            {
                sql += " AND card_lot='" + lot + "'";
            }
            else {
                sql += " limit 1";
            }

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<PoctItemDetail>(sql).ToList();
            }
        }

        public List<KeyValueModel> getNumLotList(string sn, string num)
        {
            string sql = "SELECT DISTINCT card_lot 'key', card_lot 'value' FROM poct_item WHERE device_sn = '" + sn + "' AND num = '" + num + "'";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Query<KeyValueModel>(sql).ToList();
            }
        }

        #endregion


    }
}
