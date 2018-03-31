using RemoteModel;
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
            string ids = "";
            for (int i = 0; i < SocketServer.SocketServer.testIds.Count; i++)
            {
                ids += SocketServer.SocketServer.testIds[i] + ";<br />";
            }
            this.lab.Text = ids == "" ? "当前没有连接" : ids.Remove(ids.Length - 6);
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
                ResultInfo info = SocketServer.SocketServer.sendMsg(id, this.txtMsg.Text.Trim());
                this.labMsg.Text = info.msg;
            }
        }

    }
}