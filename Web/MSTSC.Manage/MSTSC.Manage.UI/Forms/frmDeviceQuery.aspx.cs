using MSTSC.Manage.BLL;
using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.UI.Forms
{
    
    public partial class frmDeviceQuery : System.Web.UI.Page
    {
        private DeviceQueryBLL bll = new DeviceQueryBLL();

        #region 页面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
              
                BindProductType();
                gdDeviceList.DataSource = new List<QueryList>();
                gdDeviceList.DataBind();
            }
        }

        /// <summary>
        /// 仪器列表选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 根据仪器类型绑定产品类型
            var proSeriesList = Global.DeviceTypeInfos.Where(o => o.ProductType == cbxDeviceType.Text).Select(o => Common.DBdataToUI(o.ProductSeries)).Distinct().ToList();
            cbxProSeries.DataSource = proSeriesList;
            cbxProSeries.DataBind();
        }

        protected void gdDeviceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                (e.Item.FindControl("tr1") as HtmlTableRow).Attributes.Add("onclick", "GetDetail('" + (e.Item.FindControl("SN") as HtmlTableCell).InnerText + "')");
  
            }

        }

        protected void btnQuickQuery_Click(object sender, EventArgs e)
        {

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        protected void cbxProSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 根据仪器类型、产品类型绑定产品类型
         var proModelList = Global.DeviceTypeInfos.Where(o => o.ProductType == cbxDeviceType.Text && o.ProductSeries == Common.UIdataToDB(cbxProSeries.Text))
              .Select(o => o.ProductModel).Distinct().ToList();
            cbxProModel.DataSource = proModelList;
            cbxProModel.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void ddlp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pg = Convert.ToString((Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1));//获取列表框当前选中项
        }
        #endregion


        /// <summary>
        /// 仪器类型    
        /// </summary>
        private void BindProductType()
        {
            var proTypeList = Global.DeviceTypeInfos.Select(o => o.ProductType).Distinct().ToList();
            proTypeList.Insert(0, "");
            cbxDeviceType.DataSource = proTypeList;
            cbxDeviceType.DataBind();
        }

        private PagedDataSource pds()
        {
            var condiValue = getConditionValue();

            var items = bll.GetDeviceInfoBLL(condiValue);

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = items;
            pds.AllowPaging = true;//允许分页
            pds.PageSize = 1;//单页显示项数  
            pds.CurrentPageIndex = Convert.ToInt32(Request.QueryString["page"]);
            return pds;
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
            convalue.ProduceType = cbxDeviceType.Text.Trim();
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

            convalue.ReagentType = cbxReagentType.SelectedValue;

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
    }
}