using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string UserLogin(string username, string password)
        {
            if (username == "admin" && password == "123456")
            {
                return "0";
            }
            return "1";
        }
    }
}