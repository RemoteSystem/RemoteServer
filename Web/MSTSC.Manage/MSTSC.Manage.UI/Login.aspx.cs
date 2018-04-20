using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            string username = this.txt_LoginName.Text.Trim();
            string password = this.txt_Password.Text.Trim();

            if (username == "admin" && password == "123456")
            {
                Server.Transfer("Main.aspx");
            }
            else
            {
                this.LoginFail.InnerHtml = "用户名或者密码错误";
            }
        }

        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            this.txt_LoginName.Text = "";
            this.txt_Password.Text = "";
            this.LoginFail.InnerHtml = "";
        }
    }
}