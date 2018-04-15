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
    public partial class frmDeviceList : System.Web.UI.Page
    {
        private DeviceQueryBLL bll = new DeviceQueryBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductType();
                Query();
            }
        }

        protected void btnQuickQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(queryText.Text))
            {
                System.Windows.Forms.MessageBox.Show("请输入快速查询条件！");
            }
            else
            {
                var items = bll.QuickQueryBLL(queryText.Text);
                gdDeviceList.DataSource = items;
                gdDeviceList.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdSN.Value))
            {
                System.Windows.Forms.MessageBox.Show("请先在左侧仪器列表选择相应仪器！");
            }
            else
            {
                BindDetial(hdSN.Value);
            }
        }

        /// <summary>
        /// 仪器类型    
        /// </summary>
        private void BindProductType()
        {
            try
            {
                var proTypeList = Global.DeviceTypeInfos.Select(o => o.ProductType).Distinct().ToList();
                proTypeList.Insert(0, "");
                cbxDeviceType.DataSource = proTypeList;
                cbxDeviceType.DataBind();
            }
            catch (Exception ex)
            { }
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

        protected string BindDetial(string sn)
        {
            try
            {
                var result = bll.GetDeviceDetialBLL(sn);

                foreach (var item in result)
                {
                    UpdateTime.Text = Convert.ToString(item.UpdateTime);
                    SIM.Text = item.SIM;
                    Device_SN.Text = item.SN;
                    ProductSeries.Text = item.ProductSeries;
                    ProductModel.Text = item.ProductModel;
                    OEM.Text = item.OEM;
                    Agent.Text = item.Agent;
                    ReagentType.Text = item.ReagentType;
                    InstallationArea.Text = item.InstallationArea;
                    FactoryDate.Text = item.FactoryDate.ToString("yyyy-mm-dd");
                    InstallDate.Text = item.InstallDate.ToString("yyyy-mm-dd");
                    runtime_days.Text = item.runtime_days;
                    runtime_opt.Text = Convert.ToString(item.runtime_opt);
                    runtime_POWER.Text = Convert.ToString(item.runtime_power);
                    runtime_air_supply.Text = Convert.ToString(item.runtime_air_supply);
                    needle_times_impale.Text = Convert.ToString(item.needle_times_impale);
                    count_times_TOTAL.Text = Convert.ToString(item.count_times_total);
                    count_times_wb_cbc.Text = Convert.ToString(item.count_times_wb_cbc);
                    count_times_wb_cbc_crp.Text = Convert.ToString(item.count_times_wb_cbc_crp);
                    count_times_wb_crp.Text = Convert.ToString(item.count_times_wb_crp);
                    count_times_pd_cbc.Text = Convert.ToString(item.count_times_pd_cbc);
                    count_times_pd_cbc_crp.Text = Convert.ToString(item.count_times_pd_cbc_crp);
                    count_times_pd_crp.Text = Convert.ToString(item.count_times_pd_crp);
                    count_times_qc.Text = Convert.ToString(item.count_times_qc);
                    reagent_dil.Text = Convert.ToString(item.reagent_dil);
                    reagent_lh.Text = Convert.ToString(item.reagent_lh);
                    reagent_r2.Text = Convert.ToString(item.reagent_r2);
                    reagent_diff1.Text = Convert.ToString(item.reagent_diff1);
                    reagent_diff2.Text = Convert.ToString(item.reagent_diff2);
                    reagent_r1.Text = Convert.ToString(item.reagent_r1);
                    reagent_fl1.Text = Convert.ToString(item.reagent_fl1);
                    reagent_fl2.Text = Convert.ToString(item.reagent_fl2);
                    reagent_fl3.Text = Convert.ToString(item.reagent_fl3);
                    reagent_fl4.Text = Convert.ToString(item.reagent_fl4);
                    reagent_fl5.Text = Convert.ToString(item.reagent_fl5);
                    reagent_fl6.Text = Convert.ToString(item.reagent_fl6);
                    hole_times_wbc.Text = Convert.ToString(item.hole_times_wbc);
                    hole_times_rbc.Text = Convert.ToString(item.hole_times_rbc);
                    sampling_times_fault.Text = Convert.ToString(item.sampling_times_fault);
                    syringe_times_syringe_fault.Text = Convert.ToString(item.syringe_times_syringe_fault);
                    inject_times_fault.Text = Convert.ToString(item.inject_times_fault);
                    mixing_times_fault.Text = Convert.ToString(item.mixing_times_fault);
                }
            }
            catch (Exception ex)
            {
            }
            return sn;
        }

        protected void SelectButton_Command(object sender, CommandEventArgs e)
        {
            string sn = e.CommandName;
            hdSN.Value = sn;
            BindDetial(sn);
        }
    }
}