using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.UI.Forms
{
    public partial class frmDeviceManage : System.Web.UI.Page
    {
        private DeviceQueryBLL bll = new DeviceQueryBLL();

        List<ProductTypeModel> deviceInfos = new List<ProductTypeModel>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                deviceInfos = bll.ProductTypeInfoBLL();
                BindProductType();
                Query();
            }
        }

        protected void btnAddDevice_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddDevices_Click(object sender, EventArgs e)
        {

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

        }

        private void Query()
        {
            var condiValue = getConditionValue();

            var items = bll.GetDeviceInfoBLL(condiValue);
            gdDeviceList.DataSource = items;
            gdDeviceList.DataBind();
        }

        private QueryConditionModel getConditionValue()
        {
            QueryConditionModel convalue = new QueryConditionModel();
            convalue.DeviceType = cbxDeviceType.Text.Trim();
            if (allDevice.Checked)
            {
                convalue.DeviceState = MachineState.所有仪器;
            }
            else if (ConnectedDevice.Checked)
            {
                convalue.DeviceState = MachineState.已连接仪器;
            }
            else if (UnConnectedDevice.Checked)
            {
                convalue.DeviceState = MachineState.未连接仪器;
            }
            return convalue;
        }

        protected void gdDeviceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "go")
            {
                try
                {
                    TextBox tb = (TextBox)gdDeviceList.BottomPagerRow.FindControl("inPageNum");
                    int num = Int32.Parse(tb.Text);
                    GridViewPageEventArgs ea = new GridViewPageEventArgs(num - 1);
                    gdDeviceList_PageIndexChanging(null, ea);
                }
                catch
                {
                }
            }
        }

        protected void gdDeviceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gdDeviceList.PageIndex = e.NewPageIndex;
                Query();

                TextBox tb = (TextBox)gdDeviceList.BottomPagerRow.FindControl("inPageNum");
                tb.Text = (gdDeviceList.PageIndex + 1).ToString();
            }
            catch
            {
            }
        }

        protected void gdDeviceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#eed6d6'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

                e.Row.Attributes.Add("onclick", ClientScript.GetPostBackClientHyperlink(e.Row.Cells[2].FindControl("SelectButton"), ""));
            }
        }


        /// <summary>
        /// 仪器类型    
        /// </summary>
        private void BindProductType()
        {
            try
            {
                var proTypeList = deviceInfos.Select(o => o.DeviceType).Distinct().ToList();
                cbxDeviceType.DataSource = proTypeList;
                cbxDeviceType.DataBind();
            }
            catch (Exception ex)
            { }
        }

        protected void SelectButton_Command(object sender, CommandEventArgs e)
        {
            string sn = e.CommandName;
            hdSN.Value = sn;
          //  BindDetial(sn);
        }
    }
}