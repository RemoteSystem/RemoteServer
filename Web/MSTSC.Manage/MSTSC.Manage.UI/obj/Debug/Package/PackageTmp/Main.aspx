<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MSTSC.Manage.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>远程管理系统</title>
    <style type="text/css">
        body {
            padding: 0px;
            margin: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 100%">
            <div style="height: 100px; padding: 10px;">
                <div style="width: auto; height: 60px;">
                    <h1 style="position: relative; top: 25%; transform: translateY(-50%);">远程管理系统</h1>
                </div>
                <div>
                    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" ForeColor="Black">
                        <Items>
                            <asp:MenuItem Text="仪器列表" Target="ShowPage" Value="仪器列表" NavigateUrl="~/Forms/frmDeviceList.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="仪器管理" Target="ShowPage" Value="仪器管理" NavigateUrl="~/Forms/frmDeviceManage.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="查询" Target="ShowPage" Value="查询" NavigateUrl="~/Forms/frmDeviceQuery.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="统计" Target="ShowPage" Value="统计" NavigateUrl="~/Forms/frmStatistics.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="导出" Target="ShowPage" Value="导出" NavigateUrl="~/Forms/frmExport.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="备份" Target="ShowPage" Value="备份" NavigateUrl="~/Forms/frmBackUp.aspx"></asp:MenuItem>
                            <asp:MenuItem Target="ShowPage" NavigateUrl="~/Forms/frmLog.aspx" Text="日志" Value="日志"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle BackColor="#CCCCCC" ForeColor="#EE1111" />
                        <StaticSelectedStyle BackColor="#CCCCCC" />
                        <StaticMenuItemStyle BackColor="#f4f4f4" HorizontalPadding="10px" VerticalPadding="3px" />
                    </asp:Menu>
                </div>
            </div>
            <div style="width: 100%; height: 100%; position: fixed;">
                <iframe id="framePage" name="ShowPage" style="width: 100%; height: 100%; border: hidden; border-top: 2px solid #808080;" scrolling="no" src="Forms/frmDeviceQuery.aspx"></iframe>
            </div>

        </div>
    </form>
</body>
</html>
