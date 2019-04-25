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


        public int insertUser(User user)
        {
            string sql = @"INSERT INTO `user` ( userName, password, NAME, sex, age, isAdmin, isDel ) VALUES (";
            sql += "'" + user.userName + "','" + user.password + "','" + user.name + "'," + user.sex + "," + user.age + "," + user.isAdmin + "," + "0";
            sql += ");";
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Execute(sql);
            }
        }

        public int updateUser(User user)
        {
            string sql = @"UPDATE `user` set ";
            sql += " name ='" + user.name + "', sex = " + user.sex + ", age = " + user.age + ", isAdmin = " + user.isAdmin + " where id = " + user.id;
            sql += ");";
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Execute(sql);
            }
        }

        public User getUser(string id)
        {
            string sql = @"SELECT * FROM `user` where id = " + id;
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingleOrDefault<User>(sql);
            }
        }

        public User getUserByUserName(string userName)
        {
            string sql = @"SELECT * FROM `user` where userName = '" + userName + "'";
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingleOrDefault<User>(sql);
            }
        }

        public User getUserByUserNameAndPwd(string userName, string pwd, string isDel)
        {
            string sql = @"SELECT * FROM `user` where userName = '" + userName + "' and password = '" + pwd + "'";
            if (!string.IsNullOrEmpty(isDel))
            {
                sql += " and isDel = " + isDel;
            }

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingleOrDefault<User>(sql);
            }
        }

        public int delUser(string id)
        {
            string sql = @"UPDATE `user` set isDel = 1 where id = " + id;
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Execute(sql);
            }
        }

        public int enableUser(string id)
        {
            string sql = @"UPDATE `user` set isDel = 0 where id = " + id;
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Execute(sql);
            }
        }

        public int changePwd(string id, string pwd)
        {
            string sql = @"UPDATE `user` set password = '" + pwd + "' where id = " + id;
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Execute(sql);
            }
        }

        public int saveUserRights(string id, string rights)
        {
            string sql = @"DELETE FROM user_rights WHERE userId = " + id + ";";
            sql += " INSERT INTO user_rights(userId,rights) VALUES(" + id + ",'" + rights + "');";

            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.Execute(sql);
            }
        }

        public string getUserRights(string id)
        {
            string sql = @"SELECT rights FROM user_rights where userId = " + id;
            using (var conn = new MySqlConnection(Global.strConn))
            {
                return conn.QuerySingleOrDefault<string>(sql);
            }
        }

    }
}
