using MySql.Data.MySqlClient;
using RemoteModel;
using System;
using System.Collections.Generic;
using System.Data;


namespace RemoteDao
{
    public class PoctDao : BaseDao
    {
        public static int UpdateOrSavePoctItem(PoctInfo info)
        {
            if (info.category.poct.item == null) return 0;

            int num = 0;
            PoctItem item;
            for (int i = 0; i < info.category.poct.item.Count; i++)
            {
                item = info.category.poct.item[i];

                string sql = @"INSERT INTO poct_item(num,device_sn,card_name,incubate_time,analyte_1,analyte_2,analyte_3,signals,card_lot,expiry,analyte_1_params,analyte_2_params,analyte_3_params) 
                               VALUES(?num,?sn,?card_name,?incubate_time,?analyte_1,?analyte_2,?analyte_3,?signals,?card_lot,?expiry,?analyte_1_params,?analyte_2_params,?analyte_3_params) "
                            + "ON DUPLICATE KEY UPDATE ";
                if (item.card_name != null)
                    sql += ",card_name = ?card_name";
                if (item.incubate_time != null)
                    sql += ",incubate_time = ?incubate_time";
                if (item.analyte_1 != null)
                    sql += ",analyte_1 = ?analyte_1";
                if (item.analyte_2 != null)
                    sql += ",analyte_2 = ?analyte_2";
                if (item.analyte_3 != null)
                    sql += ",analyte_3 = ?analyte_3";
                if (item.signals != null)
                    sql += ",signals = ?signals";
                if (item.expiry != null)
                    sql += ",expiry = ?expiry";
                if (item.analyte_1_params != null)
                    sql += ",analyte_1_params = ?analyte_1_params";
                if (item.analyte_2_params != null)
                    sql += ",analyte_2_params = ?analyte_2_params";
                if (item.analyte_3_params != null)
                    sql += ",analyte_3_params = ?analyte_3_params";

                sql += ";";
                sql = sql.Replace("UPDATE ,", "UPDATE ");

                MySqlParameter[] parameters = { 
                                    new MySqlParameter("?num", MySqlDbType.VarChar),
                                    new MySqlParameter("?sn", MySqlDbType.VarChar),
                                    new MySqlParameter("?card_name", MySqlDbType.VarChar),
                                    new MySqlParameter("?incubate_time", MySqlDbType.Int32),
                                    new MySqlParameter("?analyte_1", MySqlDbType.VarChar),
                                    new MySqlParameter("?analyte_2", MySqlDbType.VarChar),
                                    new MySqlParameter("?analyte_3", MySqlDbType.VarChar),
                                    new MySqlParameter("?signals", MySqlDbType.Int32),
                                    new MySqlParameter("?card_lot", MySqlDbType.VarChar),
                                    new MySqlParameter("?expiry", MySqlDbType.VarChar),
                                    new MySqlParameter("?analyte_1_params", MySqlDbType.VarChar),
                                    new MySqlParameter("?analyte_2_params", MySqlDbType.VarChar),
                                    new MySqlParameter("?analyte_3_params", MySqlDbType.VarChar)};

                parameters[0].Value = item.num;
                parameters[1].Value = info.sn;
                parameters[2].Value = item.card_name;
                parameters[3].Value = item.incubate_time;
                parameters[4].Value = item.analyte_1;
                parameters[5].Value = item.analyte_2;
                parameters[6].Value = item.analyte_3;
                parameters[7].Value = item.signals;
                parameters[8].Value = item.card_lot;
                parameters[9].Value = item.expiry;
                parameters[10].Value = item.analyte_1_params;
                parameters[11].Value = item.analyte_2_params;
                parameters[12].Value = item.analyte_3_params;

                num += MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            }

            return num;
        }

        public static int UpdateOrSaveStatistics(PoctInfo info)
        {
            if (info.category.poct.statistics == null) return 0;

            string sql = "INSERT INTO poct_statistics(device_sn,sample) VALUES(?sn,?sample) "
                       + "ON DUPLICATE KEY UPDATE ";

            if (info.category.poct.statistics.sample != null)
                sql += ",sample = ?sample";

            sql += ";";
            sql = sql.Replace("UPDATE ,", "UPDATE ");

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?sample", MySqlDbType.Int32)};

            parameters[0].Value = info.sn;
            parameters[1].Value = info.category.poct.statistics.sample;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int UpdateOrSaveStatisticsItem(PoctInfo info)
        {
            if (info.category.poct.statistics == null || info.category.poct.statistics.item == null) return 0;

            int num = 0;
            PoctStatisticsItem item;

            for (int i = 0; i < info.category.poct.statistics.item.Count; i++)
            {
                item = info.category.poct.statistics.item[i];

                string sql = "INSERT INTO poct_statistics_item(num,device_sn,smpl,card_consume) VALUES(?num,?sn,?smpl,?card_consume) "
                           + "ON DUPLICATE KEY UPDATE ";

                if (item.smpl != null)
                    sql += ",smpl = ?smpl";
                if (item.card_consume != null)
                    sql += ",card_consume = ?card_consume";

                sql += ";";
                sql = sql.Replace("UPDATE ,", "UPDATE ");

                MySqlParameter[] parameters = { new MySqlParameter("?num", MySqlDbType.VarChar),
                                                new MySqlParameter("?sn", MySqlDbType.VarChar),
                                                new MySqlParameter("?smpl", MySqlDbType.Int32),
                                                new MySqlParameter("?card_consume", MySqlDbType.Int32)};

                parameters[0].Value = item.num;
                parameters[1].Value = info.sn;
                parameters[2].Value = item.smpl;
                parameters[3].Value = item.card_consume;

                num += MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            }

            return num;
        }

        public static int InsertDump(PoctInfo info)
        {
            if (info.dump == null) return 0;

            string sql = "INSERT INTO dump(device_sn,encoding,filename,data) VALUES(?sn,?encoding,?filename,?data);";

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?encoding", MySqlDbType.VarChar),
                                            new MySqlParameter("?filename", MySqlDbType.VarChar),
                                            new MySqlParameter("?data", MySqlDbType.MediumText)};

            parameters[0].Value = info.sn;
            parameters[1].Value = info.dump.encoding;
            parameters[2].Value = info.dump.filename;
            parameters[3].Value = info.dump.data;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int SaveFault(PoctInfo info)
        {
            if (info.category.poct.fault == null) return 0;

            string sql = "INSERT INTO poct_fault(device_sn,code,dttime) "
                + "VALUES(?sn,?code,?dttime) ";

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?code", MySqlDbType.VarChar),
                                            new MySqlParameter("?dttime", MySqlDbType.Timestamp)};

            int num = 0;
            for (int i = 0; i < info.category.poct.fault.Count; i++)
            {
                parameters[0].Value = info.sn;
                parameters[1].Value = info.category.poct.fault[i].code;
                parameters[2].Value = info.category.poct.fault[i].time;

                num += MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            }

            return num;
        }

        public static int SaveLog(string sn, string info)
        {
            if (string.IsNullOrEmpty(sn) || string.IsNullOrEmpty(info)) return 0;

            string sql = "insert into device_log(device_sn,content) values(?sn,?data);";

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),                                           
                                            new MySqlParameter("?data", MySqlDbType.MediumText)};

            parameters[0].Value = sn;
            parameters[1].Value = info;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

    }
}
