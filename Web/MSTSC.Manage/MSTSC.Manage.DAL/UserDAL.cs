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
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserDAL
    {
        /// <summary>
        /// 统计所有仪器
        /// </summary>
        /// <param name="conditValue"></param>
        /// <param name="pagerInfo"></param>
        /// <param name="sortInfo"></param>
        /// <returns></returns>
        public DataTable getUserList(UserQueryCondition conditValue, PagerInfo pagerInfo, SortInfo sortInfo)
        {
            StringBuilder whereSql = new StringBuilder();
            var sql = @"SELECT * FROM `user` d INNER JOIN (SELECT id FROM `user` {0} ";

            if (!string.IsNullOrEmpty(conditValue.name))
            {
                whereSql.Append(@" AND name like '%" + conditValue.name + "%'");
            }           

            if (!string.IsNullOrEmpty(conditValue.sex))
            {
                whereSql.Append(@" AND sex=" + conditValue.sex);
            }

            if (!string.IsNullOrEmpty(conditValue.isDel))
            {
                whereSql.Append(@" AND isDel=" + conditValue.isDel);
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                whereSql = new StringBuilder(" WHERE " + whereSql.ToString().Substring(4));
            }
            if (!string.IsNullOrEmpty(sortInfo.SortName))
            {
                sql += " ORDER BY " + sortInfo.SortName + " " + sortInfo.SortOrder;
            }
            sql += " LIMIT " + (pagerInfo.PageSize * (pagerInfo.CurrenetPageIndex - 1)) + "," + pagerInfo.PageSize;

            sql += " ) AS t USING(id);";

            var SqlCondit = string.Format(sql, whereSql.ToString());
            using (var conn = new MySqlConnection(Global.strConn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SqlCondit, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
                //return conn.Query<dynamic>(SqlCondit).ToList();
            }
        }

        public int getUserCount(UserQueryCondition conditValue)
        {
            StringBuilder whereSql = new StringBuilder();
            string sql = @"SELECT count(1) FROM `user` ";

            if (!string.IsNullOrEmpty(conditValue.name))
            {
                whereSql.Append(@" AND name like '%" + conditValue.name + "%'");
            }

            if (!string.IsNullOrEmpty(conditValue.sex))
            {
                whereSql.Append(@" AND sex=" + conditValue.sex);
            }

            if (!string.IsNullOrEmpty(conditValue.isDel))
            {
                whereSql.Append(@" AND isDel=" + conditValue.isDel);
            }

            if (!string.IsNullOrEmpty(whereSql.ToString()))
            {
                sql += " WHERE " + whereSql.ToString().Substring(4);
            }

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingle<int>(sql);
            }
        }

    }
}
