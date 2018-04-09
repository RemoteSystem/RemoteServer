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
        public static string strConn = "Database='remote';Data Source='120.79.244.32';User Id='root';Password='123456';charset='utf8';pooling=true";

        public List<QueryList> GetDeviceInfoDAL(QueryConditionModel conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT d.Region, d.SIM, d.SN,d.SESSIONID,p.ProductSeries, p.ProductModel
                            FROM device_info d
	                            LEFT JOIN producttype_info p ON d.ProductTypeID = p.ID
                                where 1=1 ";

            if (!string.IsNullOrEmpty(conditValue.ProduceType))
            {
                whereSql.Append(@" AND p.ProductType= :proType ");
            }

            if (conditValue.DeviceState == MachineState.已连接仪器)
            {
                whereSql.Append(@" AND d.sessionid is not null");
            }
            else if (conditValue.DeviceState == MachineState.未连接仪器)
            {
                whereSql.Append(@" AND d.sessionid is null");
            }

            if (!string.IsNullOrEmpty(conditValue.ProduceModel))
            {
                whereSql.Append(@" AND p.ProductSeries= :ProductSeries ");
            }

            if (!string.IsNullOrEmpty(conditValue.ModelType))
            {
                whereSql.Append(@" AND p.ProductModel= :ProductModel ");
            }

            if (!string.IsNullOrEmpty(conditValue.OEM))
            {
                whereSql.Append(@" AND d.OEM= :oem ");
            }

            if (!string.IsNullOrEmpty(conditValue.Agent))
            {
                whereSql.Append(@" AND d.Agent= :agent");
            }

            if (!string.IsNullOrEmpty(conditValue.ReagentType))
            {
                whereSql.Append(@" AND d.ReagentType= :ReagentType");
            }

            if (!string.IsNullOrEmpty(conditValue.Region))
            {
                whereSql.Append(@" AND d.Region=:Region ");
            }

            var parm = new
            {
                proType = conditValue.ProduceType,
                ProductSeries = conditValue.ProduceModel,
                ProductModel = conditValue.ModelType,
                oem = conditValue.OEM,
                agent = conditValue.Agent,
                ReagentType = conditValue.ReagentType,
                Region = conditValue.Region,
            };

            using (var conn = new MySqlConnection(strConn))
            {
                return conn.Query<QueryList>(sql, parm).ToList();
            }
        }

        public List<QueryList> QuickQuery()
        {
            string sql = @"SELECT d.Region, d.SIM, d.SN, p.ProductModel, d.ModelConf, d.SESSIONID
                            FROM device_info d
	                            LEFT JOIN producttype_info p ON d.ProductTypeID = p.ID";

            using (var conn = new MySqlConnection(strConn))
            {
                return conn.Query<QueryList>(sql).ToList();
            }
        }


        public void GetDeviceDetial(string sn)
        {
            string sql = @"SELECT * from device_info d
                             LEFT JOIN blood_runtime brt on d.SN=brt.device_sn 
                             left JOIN blood_count bc on d.SN=bc.device_sn
                             LEFT JOIN blood_reagent br on d.SN = br.device_sn
                             LEFT JOIN blood_module bm on d.SN=bm.device_sn
                             left join producttype_info pi on d.SN=pi.ID
                            where d.SN= :device_sn";
            using (var conn = new MySqlConnection(strConn))
            {
                conn.Query<QueryList>(sql, new { device_sn = sn }).ToList();
            }
        }

        public List<ProductTypeModel> ProductTypeInfoDAL()
        {
            var sql = @"select ID,ProductType,ProductSeries,ProductModel from producttype_info";
            using (var conn = new MySqlConnection(strConn))
            {
                return conn.Query<ProductTypeModel>(sql).ToList();
            }
        }
    }
}
