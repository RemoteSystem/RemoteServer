using MySql.Data.MySqlClient;
using RemoteModel;
using System;
using System.Collections.Generic;
using System.Data;


namespace RemoteDao
{
    public class BioDao : BaseDao
    {
        public static int UpdateOrSaveBioItem(BioInfo info)
        {
            if (info.category.bio.item == null) return 0;

            int num = 0;
            BioItemDetail item;
            for (int i = 0; i < info.category.bio.item.Count; i++)
            {
                item = info.category.bio.item[i];

                string sql = @"INSERT INTO bio_item(num,device_sn,blank_time_begin,blank_time_end,calibration_method,corrected_intercept,corrected_slope,first_reagent_volume,k_factor_value,
                                                         main_wavelength,measuring_method,reaction_direction,reaction_time_begin,reaction_time_end,sample_volume,second_reagent_volume,sub_wavelength) 
                                           VALUES(?num,?sn,?blank_time_begin,?blank_time_end,?calibration_method,?corrected_intercept,?corrected_slope,?first_reagent_volume,?k_factor_value,
                                                  ?main_wavelength,?measuring_method,?reaction_direction,?reaction_time_begin,?reaction_time_end,?sample_volume,?second_reagent_volume,?sub_wavelength) "
                            + "ON DUPLICATE KEY UPDATE ";
                if (item.blank_time_begin != null)
                    sql += ",blank_time_begin = ?blank_time_begin";
                if (item.blank_time_end != null)
                    sql += ",blank_time_end = ?blank_time_end";
                if (item.calibration_method != null)
                    sql += ",calibration_method = ?calibration_method";
                if (item.corrected_intercept != null)
                    sql += ",corrected_intercept = ?corrected_intercept";
                if (item.corrected_slope != null)
                    sql += ",corrected_slope = ?corrected_slope";
                if (item.first_reagent_volume != null)
                    sql += ",first_reagent_volume = ?first_reagent_volume";
                if (item.k_factor_value != null)
                    sql += ",k_factor_value = ?k_factor_value";
                if (item.main_wavelength != null)
                    sql += ",main_wavelength = ?main_wavelength";
                if (item.measuring_method != null)
                    sql += ",measuring_method = ?measuring_method";
                if (item.reaction_direction != null)
                    sql += ",reaction_direction = ?reaction_direction";
                if (item.reaction_time_begin != null)
                    sql += ",reaction_time_begin = ?reaction_time_begin";
                if (item.reaction_time_end != null)
                    sql += ",reaction_time_end = ?reaction_time_end";
                if (item.sample_volume != null)
                    sql += ",sample_volume = ?sample_volume";
                if (item.second_reagent_volume != null)
                    sql += ",second_reagent_volume = ?second_reagent_volume";
                if (item.sub_wavelength != null)
                    sql += ",sub_wavelength = ?sub_wavelength";

                sql += ";";
                sql = sql.Replace("UPDATE ,", "UPDATE ");

                MySqlParameter[] parameters = { 
                                    new MySqlParameter("?num", MySqlDbType.VarChar),
                                    new MySqlParameter("?sn", MySqlDbType.VarChar),
                                    new MySqlParameter("?blank_time_begin", MySqlDbType.Int32),
                                    new MySqlParameter("?blank_time_end", MySqlDbType.Int32),
                                    new MySqlParameter("?calibration_method", MySqlDbType.VarChar),
                                    new MySqlParameter("?corrected_intercept", MySqlDbType.Float),
                                    new MySqlParameter("?corrected_slope", MySqlDbType.Float),
                                    new MySqlParameter("?first_reagent_volume", MySqlDbType.Float),
                                    new MySqlParameter("?k_factor_value", MySqlDbType.Float),
                                    new MySqlParameter("?main_wavelength", MySqlDbType.Int32),
                                    new MySqlParameter("?measuring_method", MySqlDbType.VarChar),
                                    new MySqlParameter("?reaction_direction", MySqlDbType.VarChar),
                                    new MySqlParameter("?reaction_time_begin", MySqlDbType.Int32),
                                    new MySqlParameter("?reaction_time_end", MySqlDbType.Int32),
                                    new MySqlParameter("?sample_volume", MySqlDbType.Float),
                                    new MySqlParameter("?second_reagent_volume", MySqlDbType.Float),
                                    new MySqlParameter("?sub_wavelength", MySqlDbType.Int32)};

                parameters[0].Value = item.num;
                parameters[1].Value = info.sn;
                parameters[2].Value = item.blank_time_begin;
                parameters[3].Value = item.blank_time_end;
                parameters[4].Value = item.calibration_method;
                parameters[5].Value = item.corrected_intercept;
                parameters[6].Value = item.corrected_slope;
                parameters[7].Value = item.first_reagent_volume;
                parameters[8].Value = item.k_factor_value;
                parameters[9].Value = item.main_wavelength;
                parameters[10].Value = item.measuring_method;
                parameters[11].Value = item.reaction_direction;
                parameters[12].Value = item.reaction_time_begin;
                parameters[13].Value = item.reaction_time_end;
                parameters[14].Value = item.sample_volume;
                parameters[15].Value = item.second_reagent_volume;
                parameters[16].Value = item.sub_wavelength;

                num += MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            }

            return num;
        }

        public static int UpdateOrSaveStatistics(BioInfo info)
        {
            if (info.category.bio.statistics == null) return 0;

            string sql = "INSERT INTO bio_statistics(device_sn,sample) VALUES(?sn,?sample) "
                       + "ON DUPLICATE KEY UPDATE ";

            if (info.category.bio.statistics.sample != null)
                sql += ",sample = ?sample";

            sql += ";";
            sql = sql.Replace("UPDATE ,", "UPDATE ");

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?sample", MySqlDbType.Int32)};

            parameters[0].Value = info.sn;
            parameters[1].Value = info.category.bio.statistics.sample;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int UpdateOrSaveStatisticsItem(BioInfo info)
        {
            if (info.category.bio.statistics == null || info.category.bio.statistics.item == null) return 0;

            int num = 0;
            StatisticsItem item;

            for (int i = 0; i < info.category.bio.statistics.item.Count; i++)
            {
                item = info.category.bio.statistics.item[i];

                string sql = "INSERT INTO bio_statistics_item(num,device_sn,R1,R2,smpl) VALUES(?num,?sn,?r1,?r2,?smpl) "
                           + "ON DUPLICATE KEY UPDATE ";
                if (item.R1 != null)
                    sql += ",R1 = ?r1";
                if (item.R2 != null)
                    sql += ",R2 = ?r2";
                if (item.smpl != null)
                    sql += ",smpl = ?smpl";

                sql += ";";
                sql = sql.Replace("UPDATE ,", "UPDATE ");

                MySqlParameter[] parameters = { new MySqlParameter("?num", MySqlDbType.VarChar),
                                                new MySqlParameter("?sn", MySqlDbType.VarChar),
                                                new MySqlParameter("?r1", MySqlDbType.VarChar),
                                                new MySqlParameter("?r2", MySqlDbType.VarChar),
                                                new MySqlParameter("?smpl", MySqlDbType.VarChar)};

                parameters[0].Value = item.num;
                parameters[1].Value = info.sn;
                parameters[2].Value = item.R1;
                parameters[3].Value = item.R2;
                parameters[4].Value = item.smpl;

                num += MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            }

            return num;
        }

        public static int InsertDump(BioInfo info)
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

        public static int SaveFault(BioInfo info)
        {
            if (info.category.bio.fault == null) return 0;

            string sql = "INSERT INTO bio_fault(device_sn,code,dttime) "
                + "VALUES(?sn,?code,?dttime) ";

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?code", MySqlDbType.VarChar),
                                            new MySqlParameter("?dttime", MySqlDbType.Timestamp)};

            int num = 0;
            for (int i = 0; i < info.category.bio.fault.Count; i++)
            {
                parameters[0].Value = info.sn;
                parameters[1].Value = info.category.bio.fault[i].code;
                parameters[2].Value = info.category.bio.fault[i].time;

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
