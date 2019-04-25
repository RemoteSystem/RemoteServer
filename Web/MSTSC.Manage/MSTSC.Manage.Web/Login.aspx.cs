using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using MSTSC.Manage.Utils;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        [WebMethod]
        public static string UserLogin(string username, string password)
        {
            UserBLL bll = new UserBLL();

            password = EncyptHelper.MD5(password);
            User user = bll.getUserByUserNameAndPwd(username, password, "0");
            //if (username == "admin" && password == "bk#9876")
            if (null != user)
            {
                HttpSessionState session = HttpContext.Current.Session;
                session["username"] = username;
                session["userid"] = user.id;
                session["name"] = user.name;

                return "0";
            }
            return "1";
        }

        [WebMethod]
        public static void Logout(string username, string password)
        {

            HttpSessionState session = HttpContext.Current.Session;
            session["username"] = "";
            session["userid"] = "";
            session.Clear();
        }


    }
}