using MySql.Data.MySqlClient;
using RemoteModel;
using System;
using System.Collections.Generic;
using System.Data;


namespace RemoteDao
{
    public class ReceiveDao
    {
        //数据库连接字符串
        public static string Conn = "Database='remote';Data Source='120.79.244.32';User Id='root';Password='123456';charset='utf8';pooling=true";
        public static void SessionConnect(string id, DateTime dt)
        {
            //string sql = "insert into session(sessionId,startTime) values(?id,?dt)";
            //MySqlParameter[] parameters = { new MySqlParameter("?id", MySqlDbType.VarChar),
            //                              new MySqlParameter("?dt", MySqlDbType.Timestamp)};
            //parameters[0].Value = id;
            //parameters[1].Value = dt;

            //int n = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
        }

        public static int UpdateOrSaveSession(ClientInfo info)
        {
            string sql = "INSERT INTO client(sn,sim,model,region,address,hospital,sessionid,starttime) VALUES(?sn,?sim,?model,?region,?address,?hospoital,?id,?dt) "
                + "ON DUPLICATE KEY UPDATE sessionid = ?id,starttime = ?dt ;"
                + "SELECT id FROM client WHERE sessionid = ?id;";
            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?sim", MySqlDbType.VarChar),
                                            new MySqlParameter("?model", MySqlDbType.VarChar),
                                            new MySqlParameter("?region", MySqlDbType.VarChar),
                                            new MySqlParameter("?address", MySqlDbType.VarChar),
                                            new MySqlParameter("?hospoital", MySqlDbType.VarChar),
                                            new MySqlParameter("?id", MySqlDbType.VarChar),
                                            new MySqlParameter("?dt", MySqlDbType.Timestamp)};
            parameters[0].Value = info.SN;
            parameters[1].Value = info.sim;
            parameters[2].Value = info.model;
            parameters[3].Value = info.region;
            parameters[4].Value = info.addr;
            parameters[5].Value = info.hospital;
            parameters[6].Value = info.sessionid;
            parameters[7].Value = info.starttime;

            //int clientId = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            DataRow dr = MySqlHelper.ExecuteDataRow(Conn, sql, parameters);
            int clientId = 0;
            if (dr != null)
            {
                Int32.TryParse(dr["id"].ToString(), out clientId);
            }

            return clientId;
        }

        public static void SessionClose(string id)
        {
            string sql = "update client set sessionid = null,starttime = null where sessionid = ?id;";
            MySqlParameter[] parameters = { new MySqlParameter("?id", MySqlDbType.VarChar) };
            parameters[0].Value = id;

            int n = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
        }

    }
}
