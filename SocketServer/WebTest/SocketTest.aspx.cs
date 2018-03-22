using RemoteModel;
using RemoteServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
    public partial class SocketTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            this.lab.Text = SocketServer.testId == "" ? "当前没有连接" : SocketServer.testId;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string id = this.txtId.Text.Trim();
            if (id == "")
            {
                this.labMsg.Text = "请先填写ID";
            }
            else
            {
                ResultInfo info = SocketServer.sendMsg(id, this.txtMsg.Text.Trim());
                this.labMsg.Text = info.msg;
            }
        }

    }
}