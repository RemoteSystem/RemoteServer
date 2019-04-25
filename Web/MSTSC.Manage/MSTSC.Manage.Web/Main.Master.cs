using MSTSC.Manage.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSTSC.Manage.Web
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public string menuText = @"<div class='sidebar'>
                <div>
                    <ul class='nav-header'>
                        <li>血球仪</li>
                    </ul>
                    <ul class='nav'>
                        <li><a href='DeviceList.aspx'>仪器列表</a></li>
                        <li><a href='DeviceQuery.aspx'>查询</a></li>
                        <li class='has-sub'>
                            <a href='javascript:void(0);'><span>统计</span><i class='fa fa-caret-right fa-fw pull-right'></i></a>
                            <ul class='sub-menu'>
                                <li><a href='StatisticAllDevices.aspx'>所有机器</a></li>
                                <li><a href='StatisticByModel.aspx'>按模式统计</a></li>
                                <li><a href='StatisticByArea.aspx'>按区域统计</a></li>
                                <li><a href='StatisticByType.aspx'>按机型统计</a></li>
                                <li><a href='StatisticByClose.aspx'>按试剂封闭类型统计</a></li>
                                <li><a href='StatisticByOEM.aspx'>按OEM统计</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div>
                    <ul style='background-color: #81b5db; height: 1px;'></ul>
                    <ul class='nav-header'>
                        <li>生化仪</li>
                    </ul>
                    <ul class='nav'>
                        <li><a href='BioDeviceList.aspx'>仪器列表</a></li>
                        <li><a href='BioDeviceQuery.aspx'>仪器查询</a></li>
                        <li class='has-sub'>
                            <a href='javascript:void(0);'><span>统计</span><i class='fa fa-caret-right fa-fw pull-right'></i></a>
                            <ul class='sub-menu'>
                                <li><a href='BioStatisticAllDevices.aspx'>所有机器</a></li>
                                <li><a href='BioStatisticByArea.aspx'>按区域统计</a></li>
                                <li><a href='BioStatisticByType.aspx'>按机型统计</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div>
                    <ul style='background-color: #81b5db; height: 1px;'></ul>
                    <ul class='nav-header'>
                        <li>用户管理</li>
                    </ul>
                    <ul class='nav'>
                        <li><a href='UserInfo.aspx'>用户列表</a></li>
                        <li><a href='UserRights.aspx'>权限管理</a></li>
                    </ul>
                </div>
            </div>";

        protected void Page_Load(object sender, EventArgs e)
        {
            getMenu();
        }

        public void getMenu()
        {
            string userId = null != Session["userId"] ? Session["userId"].ToString() : "";
            if (userId == "1") { return; }
            menuText = @"<div class='sidebar'>";
            if (!string.IsNullOrEmpty(userId))
            {
                UserBLL bll = new UserBLL();
                string rights = bll.getUserRights(userId);
                rights = "," + rights + ",";

                if (rights.IndexOf(",1") >= 0)
                {
                    menuText += @"<div>
                    <ul class='nav-header'>
                        <li>血球仪</li>
                    </ul>
                    <ul class='nav'>";
                    if (rights.IndexOf(",11") >= 0)
                    {
                        menuText += "<li><a href='DeviceList.aspx'>仪器列表</a></li>";
                    }
                    if (rights.IndexOf(",12") >= 0)
                    {
                        menuText += "<li><a href='DeviceQuery.aspx'>查询</a></li>";
                    }
                    if (rights.IndexOf(",13") >= 0)
                    {
                        menuText += @"<li class='has-sub'>
                           <a href='javascript:void(0);'><span>统计</span><i class='fa fa-caret-right fa-fw pull-right'></i></a>
                           <ul class='sub-menu'>";
                        if (rights.IndexOf(",131") >= 0)
                        {
                            menuText += "<li><a href='StatisticAllDevices.aspx'>所有机器</a></li>";
                        }
                        if (rights.IndexOf(",132") >= 0)
                        {
                            menuText += "<li><a href='StatisticByModel.aspx'>按模式统计</a></li>";
                        }
                        if (rights.IndexOf(",133") >= 0)
                        {
                            menuText += "<li><a href='StatisticByArea.aspx'>按区域统计</a></li>";
                        }
                        if (rights.IndexOf(",134") >= 0)
                        {
                            menuText += "<li><a href='StatisticByType.aspx'>按机型统计</a></li>";
                        }
                        if (rights.IndexOf(",135") >= 0)
                        {
                            menuText += "<li><a href='StatisticByClose.aspx'>按试剂封闭类型统计</a></li>";
                        }
                        if (rights.IndexOf(",136") >= 0)
                        {
                            menuText += "<li><a href='StatisticByOEM.aspx'>按OEM统计</a></li>";
                        }

                        menuText += @"</ul></li>";
                    }
                    menuText += @"</ul></div>";
                }
                if (rights.IndexOf(",2") >= 0)
                {
                    menuText += @"<ul style='background-color: #81b5db; height: 1px;'></ul>
                    <div>
                    <ul class='nav-header'>
                        <li>血球仪</li>
                    </ul>
                    <ul class='nav'>";
                    if (rights.IndexOf(",21") >= 0)
                    {
                        menuText += "<li><a href='BioDeviceList.aspx'>仪器列表</a></li>";
                    }
                    if (rights.IndexOf(",22") >= 0)
                    {
                        menuText += "<li><a href='BioDeviceQuery.aspx'>仪器查询</a></li>";
                    }
                    if (rights.IndexOf(",23") >= 0)
                    {
                        menuText += @"<li class='has-sub'>
                           <a href='javascript:void(0);'><span>统计</span><i class='fa fa-caret-right fa-fw pull-right'></i></a>
                           <ul class='sub-menu'>";
                        if (rights.IndexOf(",231") >= 0)
                        {
                            menuText += "<li><a href='BioStatisticAllDevices.aspx'>所有机器</a></li>";
                        }
                        if (rights.IndexOf(",232") >= 0)
                        {
                            menuText += "<li><a href='BioStatisticByArea.aspx'>按区域统计</a></li>";
                        }
                        if (rights.IndexOf(",233") >= 0)
                        {
                            menuText += "<li><a href='BioStatisticByType.aspx'>按机型统计</a></li>";
                        }

                        menuText += @"</ul></li>";
                    }
                    menuText += @"</ul></div>";
                }
                if (rights.IndexOf(",3") >= 0)
                {
                    menuText += @"<ul style='background-color: #81b5db; height: 1px;'></ul>
                    <div>
                    <ul class='nav-header'>
                        <li>用户管理</li>
                    </ul>
                    <ul class='nav'>";
                    if (rights.IndexOf(",31") >= 0)
                    {
                        menuText += "<li><a href='UserInfo.aspx'>用户列表</a></li>";
                    }
                    if (rights.IndexOf(",32") >= 0)
                    {
                        menuText += "<li><a href='UserRights.aspx'>权限管理</a></li>";
                    }
                    menuText += @"</ul></div>";
                }
            }
            menuText += @"</div>";
        }

    }
}