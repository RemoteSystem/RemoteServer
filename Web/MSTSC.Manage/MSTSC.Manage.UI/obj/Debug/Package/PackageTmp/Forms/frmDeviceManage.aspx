<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="frmDeviceManage.aspx.cs" Inherits="MSTSC.Manage.UI.Forms.frmDeviceManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:100%">
      <asp:ScriptManager runat="server" ID="ScriptManager" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                 <asp:HiddenField ID="hdSN" runat="server" />
                 <div style="float:left;width:60%;">
                     <asp:Table runat="server" Height="100%">
                         <asp:TableRow Height="40px">
                             <asp:TableCell>
                                 <h2>远程诊断系统-仪器管理</h2>
                             </asp:TableCell>
                         </asp:TableRow>
                         <asp:TableRow Height="30px">
                             <asp:TableCell VerticalAlign="Middle">
                                 <asp:Button runat="server" ID="btnAddDevice" Width="100px" Text="添加仪器" OnClick="btnAddDevice_Click" />&nbsp;
                                 <asp:Button runat="server" ID="btnAddDevices"  Width="100px" Text="批量添加仪器" OnClick="btnAddDevices_Click" />
                             </asp:TableCell>
                         </asp:TableRow>
                         <asp:TableRow Height="30px">
                             <asp:TableCell>
                                 产品类型<asp:DropDownList runat="server" ID="cbxDeviceType" AutoPostBack="true" Width="100px"  />
                                 <asp:TextBox runat="server" ID="queryText" Width="120px" ToolTip="请输入仪器名称/sim卡号/仪器序列号"></asp:TextBox>
                                  <asp:Button ID="btnQuery" runat="server" Text="查询" Width="60px" OnClick="btnQuery_Click"/>
                             </asp:TableCell>
                         </asp:TableRow>
                         <asp:TableRow Height="30px">
                                 <asp:TableCell VerticalAlign="Middle">
                                    <asp:RadioButton ID="allDevice" GroupName="DeviceState" runat="server" Text="所有仪器" Checked="true" />
                                    <asp:RadioButton ID="ConnectedDevice" GroupName="DeviceState" runat="server" Text="已连接仪器" />
                                    <asp:RadioButton ID="UnConnectedDevice" GroupName="DeviceState" runat="server" Text="未连接仪器" />
                                </asp:TableCell>
                         </asp:TableRow>
                     </asp:Table>
                      <asp:GridView runat="server" ID="gdDeviceList" AutoGenerateColumns="False" CellPadding="4" AllowPaging="true" PageSize="10"
                            ForeColor="#333333" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                            BorderWidth="1px" ShowHeaderWhenEmpty="true" OnRowCommand="gdDeviceList_RowCommand" OnPageIndexChanging="gdDeviceList_PageIndexChanging"
                            OnRowDataBound="gdDeviceList_RowDataBound" PagerSettings-Mode="Numeric" EmptyDataText="没有记录！">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="仪器名称" HeaderStyle-Width="70px" DataField="DeviceName"></asp:BoundField>
                                <asp:BoundField HeaderText="SIM卡号" HeaderStyle-Width="80px" DataField="SIM"></asp:BoundField>
                                <asp:BoundField HeaderText="仪器序列号" HeaderStyle-Width="85px" DataField="SN"></asp:BoundField>
                                <asp:BoundField HeaderText="产品" HeaderStyle-Width="38px" DataField="ProductSeries"></asp:BoundField>
                                <asp:BoundField HeaderText="型号" HeaderStyle-Width="38px" DataField="ProductModel"></asp:BoundField>
                                <asp:BoundField HeaderText="连接状态" HeaderStyle-Width="70px" DataField="SESSION_ID"></asp:BoundField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="SelectButton" runat="server" CausesValidation="False" CommandName='<%#Eval("sn")%>' OnCommand="SelectButton_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerTemplate>
                                <br />
                                <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
                                <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                                <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                                <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                                <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                                到第<asp:TextBox runat="server" ID="inPageNum" Width="40px"></asp:TextBox>页
                                            <asp:Button ID="Button1" CommandName="go" runat="server" Text="GO" />
                                <br />
                            </PagerTemplate>
                        </asp:GridView>
                 </div>
                <div style="width: 60%; margin-right: 1px; float: right;">

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
