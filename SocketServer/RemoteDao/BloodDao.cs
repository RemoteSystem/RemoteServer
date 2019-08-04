using MySql.Data.MySqlClient;
using RemoteModel;
using System;
using System.Collections.Generic;
using System.Data;


namespace RemoteDao
{
    public class BloodDao : BaseDao
    {
        public static int UpdateOrSaveRuntime(BloodInfo info)
        {
            if (info.category.BLOOD == null || info.category.BLOOD.runtime == null) return 0;

            string sql = "INSERT INTO blood_runtime(device_sn,runtime_days,runtime_power,runtime_opt,runtime_air_supply) VALUES(?sn,?days,?power,?opt,?supply) "
                + "ON DUPLICATE KEY UPDATE ";
            if (info.category.BLOOD.runtime.runtime_DAYS != null)
                sql += ",runtime_days = ?days";
            if (info.category.BLOOD.runtime.runtime_POWER != null)
                sql += ",runtime_power = ?power";
            if (info.category.BLOOD.runtime.runtime_OPT != null)
                sql += ",runtime_opt = ?opt";
            if (info.category.BLOOD.runtime.runtime_AIR_SUPPLY != null)
                sql += ",runtime_air_supply = ?supply";

            sql += ";";
            sql = sql.Replace("UPDATE ,", "UPDATE ");

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?days", MySqlDbType.VarChar),
                                            new MySqlParameter("?power", MySqlDbType.VarChar),
                                            new MySqlParameter("?opt", MySqlDbType.VarChar),
                                            new MySqlParameter("?supply", MySqlDbType.VarChar)};
            parameters[0].Value = info.sn;
            parameters[1].Value = info.category.BLOOD.runtime.runtime_DAYS;
            parameters[2].Value = info.category.BLOOD.runtime.runtime_POWER;
            parameters[3].Value = info.category.BLOOD.runtime.runtime_OPT;
            parameters[4].Value = info.category.BLOOD.runtime.runtime_AIR_SUPPLY;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int UpdateOrSaveCount(BloodInfo info)
        {
            if (info.category.BLOOD == null || info.category.BLOOD.count_statistics == null) return 0;

            string sql = "INSERT INTO blood_count(device_sn,count_times_total,count_times_wb_cbc,count_times_wb_cd,count_times_wb_crp,count_times_wb_cbc_crp,count_times_wb_cd_crp,count_times_pd_cbc,count_times_pd_cd,count_times_pd_crp,count_times_pd_cbc_crp,count_times_pd_cd_crp,count_times_tipwb_cbc,count_times_tipwb_cd,count_times_qc) "
                + "VALUES(?sn,?total,?wbcbc,?wbcd,?wbcrp,?wbcbccrp,?wbcdcrp,?pdcbc,?pdcd,?pdcrp,?pdcbccrp,?pdcdcrp,?qc) "
                + "ON DUPLICATE KEY UPDATE ";
            if (info.category.BLOOD.count_statistics.count_times_TOTAL != null)
                sql += ",count_times_total = ?total";
            if (info.category.BLOOD.count_statistics.count_times_WB_CBC != null)
                sql += ",count_times_wb_cbc = ?wbcbc";
            if (info.category.BLOOD.count_statistics.count_times_WB_CD != null)
                sql += ",count_times_wb_cd = ?wbcd";
            if (info.category.BLOOD.count_statistics.count_times_WB_CRP != null)
                sql += ",count_times_wb_crp = ?wbcrp";
            if (info.category.BLOOD.count_statistics.count_times_WB_CBC_CRP != null)
                sql += ",count_times_wb_cbc_crp = ?wbcbccrp";
            if (info.category.BLOOD.count_statistics.count_times_WB_CD_CRP != null)
                sql += ",count_times_wb_cd_crp = ?wbcdcrp";
            if (info.category.BLOOD.count_statistics.count_times_PD_CBC != null)
                sql += ",count_times_pd_cbc = ?pdcbc";
            if (info.category.BLOOD.count_statistics.count_times_PD_CD != null)
                sql += ",count_times_pd_cd = ?pdcd";
            if (info.category.BLOOD.count_statistics.count_times_PD_CRP != null)
                sql += ",count_times_pd_crp = ?pdcrp";
            if (info.category.BLOOD.count_statistics.count_times_PD_CBC_CRP != null)
                sql += ",count_times_pd_cbc_crp = ?pdcbccrp";
            if (info.category.BLOOD.count_statistics.count_times_PD_CD_CRP != null)
                sql += ",count_times_pd_cd_crp = ?pdcdcrp";
            if (info.category.BLOOD.count_statistics.count_times_tipwb_cbc != null)
                sql += ",count_times_tipwb_cbc = ?count_times_tipwb_cbc";
            if (info.category.BLOOD.count_statistics.count_times_tipwb_cd != null)
                sql += ",count_times_tipwb_cd = ?count_times_tipwb_cd";
            if (info.category.BLOOD.count_statistics.count_times_QC != null)
                sql += ",count_times_qc = ?qc";

            sql += ";";
            sql = sql.Replace("UPDATE ,", "UPDATE ");

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?total", MySqlDbType.VarChar),
                                            new MySqlParameter("?wbcbc", MySqlDbType.VarChar),
                                            new MySqlParameter("?wbcd", MySqlDbType.VarChar),
                                            new MySqlParameter("?wbcrp", MySqlDbType.VarChar),
                                            new MySqlParameter("?wbcbccrp", MySqlDbType.VarChar),
                                            new MySqlParameter("?wbcdcrp", MySqlDbType.VarChar),
                                            new MySqlParameter("?pdcbc", MySqlDbType.VarChar),
                                            new MySqlParameter("?pdcd", MySqlDbType.VarChar),
                                            new MySqlParameter("?pdcrp", MySqlDbType.VarChar),
                                            new MySqlParameter("?pdcbccrp", MySqlDbType.VarChar),
                                            new MySqlParameter("?pdcdcrp", MySqlDbType.VarChar),
                                            new MySqlParameter("?count_times_tipwb_cbc", MySqlDbType.VarChar),
                                            new MySqlParameter("?count_times_tipwb_cd", MySqlDbType.VarChar),
                                            new MySqlParameter("?qc", MySqlDbType.VarChar)};

            parameters[0].Value = info.sn;
            parameters[1].Value = info.category.BLOOD.count_statistics.count_times_TOTAL;
            parameters[2].Value = info.category.BLOOD.count_statistics.count_times_WB_CBC;
            parameters[3].Value = info.category.BLOOD.count_statistics.count_times_WB_CD;
            parameters[4].Value = info.category.BLOOD.count_statistics.count_times_WB_CRP;
            parameters[5].Value = info.category.BLOOD.count_statistics.count_times_WB_CBC_CRP;
            parameters[6].Value = info.category.BLOOD.count_statistics.count_times_WB_CD_CRP;
            parameters[7].Value = info.category.BLOOD.count_statistics.count_times_PD_CBC;
            parameters[8].Value = info.category.BLOOD.count_statistics.count_times_PD_CD;
            parameters[9].Value = info.category.BLOOD.count_statistics.count_times_PD_CRP;
            parameters[10].Value = info.category.BLOOD.count_statistics.count_times_PD_CBC_CRP;
            parameters[11].Value = info.category.BLOOD.count_statistics.count_times_PD_CD_CRP;
            parameters[12].Value = info.category.BLOOD.count_statistics.count_times_tipwb_cbc;
            parameters[13].Value = info.category.BLOOD.count_statistics.count_times_tipwb_cd;
            parameters[14].Value = info.category.BLOOD.count_statistics.count_times_QC;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int InsertCountDetail(BloodInfo info)
        {
            if (info.category.BLOOD == null || info.category.BLOOD.count_statistics == null) return 0;

            string base_sql = "INSERT INTO blood_count_detail(device_sn,device_count,device_count_type) VALUES(\"{0}\",{1},\"{2}\"); ";
            string sql="";
            
            if (info.category.BLOOD.count_statistics.count_times_TOTAL != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_TOTAL, "count_times_total");
            if (info.category.BLOOD.count_statistics.count_times_WB_CBC != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_WB_CBC, "count_times_wb_cbc");
            if (info.category.BLOOD.count_statistics.count_times_WB_CD != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_WB_CD, "count_times_wb_cd");
            if (info.category.BLOOD.count_statistics.count_times_WB_CRP != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_WB_CRP, "count_times_wb_crp");
            if (info.category.BLOOD.count_statistics.count_times_WB_CBC_CRP != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_WB_CBC_CRP, "count_times_wb_cbc_crp");
            if (info.category.BLOOD.count_statistics.count_times_WB_CD_CRP != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_WB_CD_CRP, "count_times_wb_cd_crp");
            if (info.category.BLOOD.count_statistics.count_times_PD_CBC != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_PD_CBC, "count_times_pd_cbc");
            if (info.category.BLOOD.count_statistics.count_times_PD_CD != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_PD_CD, "count_times_pd_cd");
            if (info.category.BLOOD.count_statistics.count_times_PD_CRP != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_PD_CRP, "count_times_pd_crp");
            if (info.category.BLOOD.count_statistics.count_times_PD_CBC_CRP != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_PD_CBC_CRP, "count_times_pd_cbc_crp");
            if (info.category.BLOOD.count_statistics.count_times_PD_CD_CRP != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_PD_CD_CRP, "count_times_pd_cd_crp");
            if (info.category.BLOOD.count_statistics.count_times_tipwb_cbc != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_tipwb_cbc, "count_times_tipwb_cbc");
            if (info.category.BLOOD.count_statistics.count_times_tipwb_cd != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_tipwb_cd, "count_times_tipwb_cd");
            if (info.category.BLOOD.count_statistics.count_times_QC != null)
                sql += string.Format(base_sql, info.sn, info.category.BLOOD.count_statistics.count_times_QC, "count_times_qc");

            MySqlParameter[] parameters = {};            

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int UpdateOrSaveReagent(BloodInfo info)
        {
            if (info.category.BLOOD == null || info.category.BLOOD.reagent == null) return 0;

            string sql = "INSERT INTO blood_reagent(device_sn,reagent_type,reagent_dil,reagent_lh,reagent_r1,reagent_r2,reagent_diff1,reagent_diff2,reagent_fl1,reagent_fl2,reagent_fl3,reagent_fl4,reagent_fl5,reagent_fl6) "
                + "VALUES(?sn,?reagent_type,?dil,?lh,?r1,?r2,?diff1,?diff2,?fl1,?fl2,?fl3,?fl4,?fl5,?fl6) "
                + "ON DUPLICATE KEY UPDATE ";
            if (info.category.BLOOD.reagent.reagent_type != null)
                sql += ",reagent_type = ?reagent_type";
            if (info.category.BLOOD.reagent.reagent_DIL != null)
                sql += ",reagent_dil = ?dil";
            if (info.category.BLOOD.reagent.reagent_LH != null)
                sql += ",reagent_lh = ?lh";
            if (info.category.BLOOD.reagent.reagent_R1 != null)
                sql += ",reagent_r1 = ?r1";
            if (info.category.BLOOD.reagent.reagent_R2 != null)
                sql += ",reagent_r2 = ?r2";
            if (info.category.BLOOD.reagent.reagent_DIFF1 != null)
                sql += ",reagent_diff1 = ?diff1";
            if (info.category.BLOOD.reagent.reagent_DIFF2 != null)
                sql += ",reagent_diff2 = ?diff2";
            if (info.category.BLOOD.reagent.reagent_FL1 != null)
                sql += ",reagent_fl1 = ?fl1";
            if (info.category.BLOOD.reagent.reagent_FL2 != null)
                sql += ",reagent_fl2 = ?fl2";
            if (info.category.BLOOD.reagent.reagent_FL3 != null)
                sql += ",reagent_fl3 = ?fl3";
            if (info.category.BLOOD.reagent.reagent_FL4 != null)
                sql += ",reagent_fl4 = ?fl4";
            if (info.category.BLOOD.reagent.reagent_FL5 != null)
                sql += ",reagent_fl5 = ?fl5";
            if (info.category.BLOOD.reagent.reagent_FL6 != null)
                sql += ",reagent_fl6 = ?fl6";

            sql += ";";
            sql = sql.Replace("UPDATE ,", "UPDATE ");

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?reagent_type", MySqlDbType.VarChar),
                                            new MySqlParameter("?dil", MySqlDbType.VarChar),
                                            new MySqlParameter("?lh", MySqlDbType.VarChar),
                                            new MySqlParameter("?r1", MySqlDbType.VarChar),
                                            new MySqlParameter("?r2", MySqlDbType.VarChar),
                                            new MySqlParameter("?diff1", MySqlDbType.VarChar),
                                            new MySqlParameter("?diff2", MySqlDbType.VarChar),
                                            new MySqlParameter("?fl1", MySqlDbType.VarChar),
                                            new MySqlParameter("?fl2", MySqlDbType.VarChar),
                                            new MySqlParameter("?fl3", MySqlDbType.VarChar),
                                            new MySqlParameter("?fl4", MySqlDbType.VarChar),
                                            new MySqlParameter("?fl5", MySqlDbType.VarChar),
                                            new MySqlParameter("?fl6", MySqlDbType.VarChar)};

            parameters[0].Value = info.sn;
            parameters[1].Value = info.category.BLOOD.reagent.reagent_type;
            parameters[2].Value = info.category.BLOOD.reagent.reagent_DIL;
            parameters[3].Value = info.category.BLOOD.reagent.reagent_LH;
            parameters[4].Value = info.category.BLOOD.reagent.reagent_R1;
            parameters[5].Value = info.category.BLOOD.reagent.reagent_R2;
            parameters[6].Value = info.category.BLOOD.reagent.reagent_DIFF1;
            parameters[7].Value = info.category.BLOOD.reagent.reagent_DIFF2;
            parameters[8].Value = info.category.BLOOD.reagent.reagent_FL1;
            parameters[9].Value = info.category.BLOOD.reagent.reagent_FL2;
            parameters[10].Value = info.category.BLOOD.reagent.reagent_FL3;
            parameters[11].Value = info.category.BLOOD.reagent.reagent_FL4;
            parameters[12].Value = info.category.BLOOD.reagent.reagent_FL5;
            parameters[13].Value = info.category.BLOOD.reagent.reagent_FL6;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int UpdateOrSaveModule(BloodInfo info)
        {
            if (info.category.BLOOD == null || info.category.BLOOD.module_statistics == null) return 0;

            string sql = "INSERT INTO blood_module(device_sn,hole_times_wbc,hole_times_rbc,needle_times_impale,sampling_times_fault,syringe_times_syringe_fault,inject_times_fault,mixing_times_fault) "
                + "VALUES(?sn,?wbc,?rbc,?needle,?sampling,?syringe,?inject,?mixing) "
                + "ON DUPLICATE KEY UPDATE ";
            if (info.category.BLOOD.module_statistics.hole_times_WBC != null)
                sql += ",hole_times_wbc = ?wbc";
            if (info.category.BLOOD.module_statistics.hole_times_RBC != null)
                sql += ",hole_times_rbc = ?rbc";
            if (info.category.BLOOD.module_statistics.needle_times_impale != null)
                sql += ",needle_times_impale = ?needle";
            if (info.category.BLOOD.module_statistics.sampling_times_fault != null)
                sql += ",sampling_times_fault = ?sampling";
            if (info.category.BLOOD.module_statistics.Syringe_times_syringe_fault != null)
                sql += ",syringe_times_syringe_fault = ?syringe";
            if (info.category.BLOOD.module_statistics.inject_times_fault != null)
                sql += ",inject_times_fault = ?inject";
            if (info.category.BLOOD.module_statistics.mixing_times_fault != null)
                sql += ",mixing_times_fault = ?mixing";

            sql += ";";
            sql = sql.Replace("UPDATE ,", "UPDATE ");

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?wbc", MySqlDbType.VarChar),
                                            new MySqlParameter("?rbc", MySqlDbType.VarChar),
                                            new MySqlParameter("?needle", MySqlDbType.VarChar),
                                            new MySqlParameter("?sampling", MySqlDbType.VarChar),
                                            new MySqlParameter("?syringe", MySqlDbType.VarChar),
                                            new MySqlParameter("?inject", MySqlDbType.VarChar),
                                            new MySqlParameter("?mixing", MySqlDbType.VarChar)};

            parameters[0].Value = info.sn;
            parameters[1].Value = info.category.BLOOD.module_statistics.hole_times_WBC;
            parameters[2].Value = info.category.BLOOD.module_statistics.hole_times_RBC;
            parameters[3].Value = info.category.BLOOD.module_statistics.needle_times_impale;
            parameters[4].Value = info.category.BLOOD.module_statistics.sampling_times_fault;
            parameters[5].Value = info.category.BLOOD.module_statistics.Syringe_times_syringe_fault;
            parameters[6].Value = info.category.BLOOD.module_statistics.inject_times_fault;
            parameters[7].Value = info.category.BLOOD.module_statistics.mixing_times_fault;

            int num = MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);

            return num;
        }

        public static int SaveFault(BloodInfo info)
        {
            if (info.category.BLOOD == null || info.category.BLOOD.fault == null) return 0;

            string sql = "INSERT INTO blood_fault(device_sn,code,dttime) "
                + "VALUES(?sn,?code,?dttime) ";

            MySqlParameter[] parameters = { new MySqlParameter("?sn", MySqlDbType.VarChar),
                                            new MySqlParameter("?code", MySqlDbType.VarChar),
                                            new MySqlParameter("?dttime", MySqlDbType.Timestamp)};

            int num = 0;
            for (int i = 0; i < info.category.BLOOD.fault.Count; i++)
            {
                parameters[0].Value = info.sn;
                parameters[1].Value = info.category.BLOOD.fault[i].code;
                parameters[2].Value = info.category.BLOOD.fault[i].time;

                num += MySqlHelper.ExecuteNonQuery(Conn, sql, parameters);
            }
            
            return num;
        }

    }
}
