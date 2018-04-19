using MySql.Data.MySqlClient;
using RemoteModel;
using System;
using System.Collections.Generic;
using System.Data;


namespace RemoteDao
{
    public class ServerDao : BaseDao
    {
        public static void ServerStop()
        {
            string sql = "UPDATE device_info SET sessionid = NULL,starttime = NULL;";
            MySqlParameter[] parameters = {};

            int n = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
        }        
    }
}
