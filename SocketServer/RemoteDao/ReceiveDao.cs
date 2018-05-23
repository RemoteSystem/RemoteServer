﻿using MySql.Data.MySqlClient;
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
            string sql = "INSERT INTO device_info(SN,SIM,Region,Hospital,Address,Model,DeviceType,ProductSeries,ProductModel,OEM,Agent,ReagentType,FactoryDate,InstallDate,SoftVersion,UpdateTime,sessionid,starttime) "
                + "VALUES(?sn,?sim,?region,?hospoital,?address,?model,'血液细胞分析仪',?series,?pmodel,?oem,?agent,?reatype,?dtfactory,?dtinstall,?version,?dtupdate,?id,?dt) "
                + "ON DUPLICATE KEY UPDATE ";

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
                sql += ",Model = ?model";
            }
            if (info.category != null && info.category.BLOOD != null)
            {
                if (info.category.BLOOD.series != null)
                {
                    sql += ",ProductSeries = ?series";
                }
                if (info.category.BLOOD.product_model != null)
                {
                    sql += ",ProductModel = ?pmodel";
                }
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
                                            new MySqlParameter("?series", MySqlDbType.VarChar),
                                            new MySqlParameter("?pmodel", MySqlDbType.VarChar),
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
            parameters[5].Value = info.model != null ? info.model.ToUpper() : null;
            if (info.category == null || info.category.BLOOD == null)
            {
                parameters[6].Value = DBNull.Value;
                parameters[7].Value = DBNull.Value;
                parameters[8].Value = "";
                parameters[9].Value = "";
                parameters[10].Value = "";
                parameters[11].Value = DBNull.Value;
                parameters[12].Value = DBNull.Value;
                parameters[13].Value = "";
                parameters[14].Value = DBNull.Value;
            }
            else
            {
                parameters[6].Value = info.category.BLOOD.series;
                parameters[7].Value = info.category.BLOOD.product_model;
                parameters[8].Value = info.category.BLOOD.OEM;
                parameters[9].Value = info.category.BLOOD.agent;
                parameters[10].Value = info.category.BLOOD.reagent_type;
                parameters[11].Value = info.category.BLOOD.date_factory;
                parameters[12].Value = info.category.BLOOD.date_install;
                parameters[13].Value = info.category.BLOOD.soft_main_version;
                parameters[14].Value = info.category.BLOOD.update_time;
            }
            parameters[15].Value = info.sessionid;
            parameters[16].Value = info.starttime;

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

    }
}
