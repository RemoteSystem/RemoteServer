using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage
{
    public partial class Main : System.Web.UI.Page
    {
        private DeviceQueryBLL bll = new DeviceQueryBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Global.DeviceTypeInfos = bll.ProductTypeInfoBLL();
            }
        }
    }
}