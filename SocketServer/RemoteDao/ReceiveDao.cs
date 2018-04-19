using MySql.Data.MySqlClient;
using RemoteModel;
using System;
using System.Collections.Generic;
using System.Data;


namespace RemoteDao
{
    public class ReceiveDao : BaseDao
    {
        public static void SessionConnect(string id, DateTime dt)
        {
            //string sql = "insert into session(sessionId,startTime) values(?id,?dt)";
            //MySqlParameter[] parameters = { new MySqlParameter("?id", MySqlDbType.VarChar),
            //                              new MySqlParameter("?dt", MySqlDbType.Timestamp)};
            //parameters[0].Value = id;
            //parameters[1].Value = dt;

            //int n = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
        }

        public static int UpdateOrSaveSession(JsonInfo info)
        {
            string sql = "INSERT INTO device_info(SN,SIM,Region,Hospital,Address,ModelConf,ProductTypeID,OEM,Agent,ReagentType,FactoryDate,InstallDate,SoftVersion,UpdateTime,sessionid,starttime) "
                + "VALUES(?sn,?sim,?region,?hospoital,?address,?model,?ptypeid,?oem,?agent,?reatype,?dtfactory,?dtinstall,?version,?dtupdate,?id,?dt) "
                + "ON DUPLICATE KEY UPDATE ";

            int ptypeid = UpdateOrSaveProductType(info);

            if (info.sim != null)
            {
                sql += ",SIM = ?sim";
            }
            if (info.region != null)
            {
                sql += ",Region = ?region";
            }
            if (info.hospital != null)
            {
                sql += ",Hospital = ?hospoital";
            }
            if (info.addr != null)
            {
                sql += ",Address = ?address";
            }
            if (info.model != null)
            {
                sql += ",ModelConf = ?model";
            }
            if (ptypeid > 0)
            {
                sql += ",ProductTypeID = ?ptypeid";
            }
            if (info.category != null && info.category.BLOOD != null)
            {
                if (info.category.BLOOD.OEM != null)
                {
                    sql += ",OEM = ?oem";
                }
                if (info.category.BLOOD.agent != null)
                {
                    sql += ",Agent = ?agent";
                }
                if (info.category.BLOOD.reagent_type != null)
                {
                    sql += ",ReagentType = ?reatype";
                }
                if (info.category.BLOOD.date_factory != null && info.category.BLOOD.date_factory != DateTime.MinValue)
                {
                    sql += ",FactoryDate = ?dtfactory";
                }
                if (info.category.BLOOD.date_install != null && info.category.BLOOD.date_install != DateTime.MinValue)
                {
                    sql += ",InstallDate = ?dtinstall";
                }
                if (info.category.BLOOD.soft_main_version != null)
                {
                    sql += ",SoftVersion = ?version";
                }
                if (info.category.BLOOD.update_time != null && info.category.BLOOD.update_time != DateTime.MinValue)
                {
                    sql += ",UpdateTime = ?dtupdate";
                }
            }
            sql += ",sessionid = ?id,starttime = ?dt; ";
            sql = sql.Replace("UPDATE ,", "UPDATE ");

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?sim", MySqlDbType.VarChar),                                            
                                            new MySqlParameter("?region", MySqlDbType.VarChar),                                            
                                            new MySqlParameter("?hospoital", MySqlDbType.VarChar),
                                            new MySqlParameter("?address", MySqlDbType.VarChar),
                                            new MySqlParameter("?model", MySqlDbType.VarChar),
                                            new MySqlParameter("?ptypeid", MySqlDbType.VarChar),
                                            new MySqlParameter("?oem", MySqlDbType.VarChar),
                                            new MySqlParameter("?agent", MySqlDbType.VarChar),
                                            new MySqlParameter("?reatype", MySqlDbType.VarChar),
                                            new MySqlParameter("?dtfactory", MySqlDbType.Timestamp),
                                            new MySqlParameter("?dtinstall", MySqlDbType.Timestamp),
                                            new MySqlParameter("?version", MySqlDbType.VarChar),
                                            new MySqlParameter("?dtupdate", MySqlDbType.Timestamp),
                                            new MySqlParameter("?id", MySqlDbType.VarChar),
                                            new MySqlParameter("?dt", MySqlDbType.Timestamp)};
            parameters[0].Value = info.sn;
            parameters[1].Value = info.sim;
            parameters[2].Value = info.region;
            parameters[3].Value = info.hospital;
            parameters[4].Value = info.addr;
            parameters[5].Value = info.model;
            parameters[6].Value = ptypeid;
            if (info.category == null || info.category.BLOOD == null)
            {
                parameters[7].Value = "";
                parameters[8].Value = "";
                parameters[9].Value = "";
                parameters[10].Value = DBNull.Value;
                parameters[11].Value = DBNull.Value;
                parameters[12].Value = "";
                parameters[13].Value = DBNull.Value;
            }
            else
            {
                parameters[7].Value = info.category.BLOOD.OEM;
                parameters[8].Value = info.category.BLOOD.agent;
                parameters[9].Value = info.category.BLOOD.reagent_type;
                parameters[10].Value = info.category.BLOOD.date_factory;
                parameters[11].Value = info.category.BLOOD.date_install;
                parameters[12].Value = info.category.BLOOD.soft_main_version;
                parameters[13].Value = info.category.BLOOD.update_time;
            }
            parameters[14].Value = info.sessionid;
            parameters[15].Value = info.starttime;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            return num;
        }

        public static void SessionClose(string id)
        {
            string sql = "update device_info set sessionid = null,starttime = null where sessionid = ?id;";
            MySqlParameter[] parameters = { new MySqlParameter("?id", MySqlDbType.VarChar) };
            parameters[0].Value = id;

            int n = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
        }

        public static int UpdateOrSaveProductType(JsonInfo info)
        {
            int id = 0;

            MySqlParameter[] parameters = { new MySqlParameter("?ptype", MySqlDbType.VarChar),
                                            new MySqlParameter("?pseries", MySqlDbType.VarChar),                                            
                                            new MySqlParameter("?pmodel", MySqlDbType.VarChar)};

            if (info.category != null && info.category.BLOOD != null)
            {
                parameters[0].Value = "血球分析仪";
                parameters[1].Value = info.category.BLOOD.series;
                parameters[2].Value = info.category.BLOOD.product_model;
            }
            else
            {
                return id;
            }

            string sql = "SELECT ID as id FROM producttype_info WHERE ProductType = ?ptype AND ProductSeries = ?pseries AND ProductModel = ?pmodel;";
            DataRow dr = MySqlHelper.ExecuteDataRow(Conn, sql, parameters);

            if (null != dr)
            {
                Int32.TryParse(dr["id"].ToString(), out id);
            }

            if (id > 0)
            {
                return id;
            }

            sql = "INSERT INTO producttype_info(ProductType,ProductSeries,ProductModel) VALUES(?ptype,?pseries,?pmodel);"
                + "SELECT ID as id FROM producttype_info WHERE ProductType = ?ptype AND ProductSeries = ?pseries AND ProductModel = ?pmodel;";

            dr = MySqlHelper.ExecuteDataRow(Conn, sql, parameters);

            if (null != dr)
            {
                Int32.TryParse(dr["id"].ToString(), out id);
            }

            return id;
        }

    }
}
