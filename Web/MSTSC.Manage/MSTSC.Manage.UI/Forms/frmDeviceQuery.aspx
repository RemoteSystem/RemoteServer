<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="frmDeviceQuery.aspx.cs" Inherits="MSTSC.Manage.UI.Forms.frmDeviceQuery" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/QueryCss.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 100%;">
            <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
            <asp:UpdatePanel ID="updatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdSN" runat="server" />
                    <div style="float: left; width: 39%;">
                        <asp:Table runat="server" Height="100%">
                            <asp:TableRow Height="40px">
                                <asp:TableCell>
                                    <h2>远程诊断系统-仪器列表</h2>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="30px">
                                <asp:TableCell VerticalAlign="Middle" ColumnSpan="2">
                                    产品类型<asp:DropDownList runat="server" AutoPostBack="true" ID="cbxDeviceType" Width="100px" OnSelectedIndexChanged="cbxDeviceType_SelectedIndexChanged" />
                                    <asp:TextBox runat="server" ID="queryText" Width="120px" ToolTip="请输入仪器名称/sim卡号/仪器序列号"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnQuickQuery" Text="快速查询" OnClick="btnQuickQuery_Click" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="30px">
                                <asp:TableCell VerticalAlign="Middle" ColumnSpan="2">
                                    <asp:RadioButton ID="allDevice" GroupName="DeviceState" runat="server" Text="所有仪器" Checked="true" />
                                    <asp:RadioButton ID="ConnectedDevice" GroupName="DeviceState" runat="server" Text="已连接仪器" />
                                    <asp:RadioButton ID="UnConnectedDevice" GroupName="DeviceState" runat="server" Text="未连接仪器" />
                                    &nbsp;
                                    <asp:Button ID="btnQuery" runat="server" Text="查询" Width="60px" OnClick="btnQuery_Click" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="30">
                                <asp:TableCell>
                                    产品类型<asp:DropDownList ID="cbxProSeries" runat="server" AutoPostBack="True" Width="100px" OnSelectedIndexChanged="cbxProSeries_SelectedIndexChanged" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    产品型号<asp:DropDownList ID="cbxProModel" runat="server" Width="110px" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="30">
                                <asp:TableCell>
                                    OEM代号<asp:DropDownList ID="cbxOEM" runat="server" Width="100px" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    代理商代号<asp:DropDownList ID="cbxAgent" runat="server" Width="100px" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="30">
                                <asp:TableCell>
                                    封闭试剂类型
                                    <asp:DropDownList ID="cbxReagentType" runat="server" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="open">开放</asp:ListItem>
                                        <asp:ListItem Value="close">封闭</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:TableCell>
                                <asp:TableCell>
                                    装机区域<asp:DropDownList runat="server" Width="100px" />                             
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <asp:GridView runat="server" ID="gdDeviceList" AutoGenerateColumns="False" CellPadding="4" AllowPaging="true" PageSize="10"
                            ForeColor="#333333" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                            BorderWidth="1px" ShowHeaderWhenEmpty="true" OnRowCommand="gdDeviceList_RowCommand" OnPageIndexChanging="gdDeviceList_PageIndexChanging"
                            OnRowDataBound="gdDeviceList_RowDataBound" PagerSettings-Mode="Numeric" EmptyDataText="没有记录！">

                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="仪器名称" HeaderStyle-Width="70px" DataField="Region"></asp:BoundField>
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
                        <asp:Panel ID="DeviceDetialPanel" Height="530px" Width="100%" runat="server" ScrollBars="Vertical">
                            <asp:Table runat="server" Width="98%">
                                <asp:TableRow>
                                    <asp:TableCell Width="50%">
                                   <h2>远程诊断系统-仪器名称</h2>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" Width="50%">
                                        数据更新时间：<asp:TextBox Enabled="false" ReadOnly="true" runat="server" ID="UpdateTime" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    基本信息
                                    </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                        <asp:Button runat="server" Width="60px" ID="btnRefresh" Text="刷新" OnClick="btnRefresh_Click" />

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table runat="server" Width="100%">
                                            <asp:TableRow Height="30px">
                                                <asp:TableCell>
                                                SIM卡号
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="SIM" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                仪器序列号
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="Device_SN" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                产品类型
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="ProductSeries" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                产品型号
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="ProductModel" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30px">
                                                <asp:TableCell>
                                                OEM代号
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="OEM" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                代理商代号
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="Agent" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                封闭试剂类型
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="ReagentType" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                装机区域
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="InstallationArea" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30px">
                                                <asp:TableCell>
                                                出厂日期
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="FactoryDate" Enabled="false" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                装机日期
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="5">
                                                    <asp:TextBox ID="InstallDate" Enabled="false" Width="100px"  runat="server" ReadOnly="true"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell ColumnSpan="6" Height="40px" VerticalAlign="Bottom">
                                                仪器运行信息
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30px">
                                                <asp:TableCell>
                                                开机天数
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="runtime_days" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                激光管运行时间
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="runtime_opt" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                仪器运行时间
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="runtime_POWER" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30px">
                                                <asp:TableCell>
                                                气源运行时间
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="runtime_air_supply" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell />
                                                <asp:TableCell />
                                                <asp:TableCell>
                                                采样针穿刺次数
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="5">
                                                    <asp:TextBox ID="needle_times_impale" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table runat="server" Width="100%">
                                            <asp:TableRow Height="40px" VerticalAlign="Bottom">
                                                <asp:TableCell ColumnSpan="6">
                                                计数信息统计
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                计数总次数
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="5">
                                                    <asp:TextBox ID="count_times_TOTAL" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                全血-CBC
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="count_times_wb_cbc" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                全血-CBC+CRP
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="count_times_wb_cbc_crp" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                全血-CRP
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="count_times_wb_crp" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                预稀释-CBC
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="count_times_pd_cbc" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预稀释-CBC+CRP
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="count_times_pd_cbc_crp" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预稀释-CRP
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="count_times_pd_crp" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                全血-CBC+5DIFF
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:TextBox ID="count_times_wb_cd" Enabled="false" runat="server" Width="80px" ReadOnly="true"/>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                全血-CBC+5DIFF+CRP
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:TextBox ID="count_times_wb_cd_crp"  Enabled="false" runat="server" Width="80px" ReadOnly="true"/>
                                                </asp:TableCell>
                                                <asp:TableCell />
                                                <asp:TableCell />
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                预稀释-CBC+5DIFF
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:TextBox ID="count_times_pd_cd"  Enabled="false" runat="server" ReadOnly="true" Width="80px"/>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预稀释-CBC+5DIFF+CRP
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:TextBox ID="count_times_pd_cd_crp" Enabled="false" runat="server" ReadOnly="true" Width="80px"/>
                                                </asp:TableCell>
                                                <asp:TableCell />
                                                <asp:TableCell />
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                质控样本数
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="5">
                                                    <asp:TextBox ID="count_times_qc" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table runat="server" Width="100%">
                                            <asp:TableRow Height="40">
                                                <asp:TableCell ColumnSpan="6">
                                                试剂耗量统计
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                稀释液
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_dil" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                LH溶血剂
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_lh" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                CRP-R2
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_r2" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                DIFF1
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_diff1" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                DIFF2
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_diff2" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                CRP-R1
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_r1" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                预留1
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_fl1" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预留2
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_fl2" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预留3
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_fl3" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                预留4
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_fl4" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预留5
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_fl5" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预留6
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="reagent_fl6" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                预留7
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:TextBox  Enabled="false" runat="server" ReadOnly="true" Width="80px"/>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预留8
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:TextBox  Enabled="false" runat="server" ReadOnly="true" Width="80px"/>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                预留9
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:TextBox  Enabled="false" runat="server" ReadOnly="true" Width="80px"/>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>
                                                预留10
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="5">
                                                <asp:TextBox  Enabled="false" runat="server" ReadOnly="true" Width="80px"/>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table runat="server" Width="100%">
                                            <asp:TableRow Height="40">
                                                <asp:TableCell ColumnSpan="6">
                                                 故障统计信息
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>WBC堵孔次数</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="hole_times_wbc" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>RBC堵孔次数</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="hole_times_rbc" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>采样组件故障次数</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="sampling_times_fault" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="30">
                                                <asp:TableCell>注射器故障次数</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="syringe_times_syringe_fault" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>自动进样组件故障</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="inject_times_fault" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                                <asp:TableCell>混匀组件故障次数</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="mixing_times_fault" Enabled="false" Width="80px" runat="server" ReadOnly="true" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                    <asp:Table runat="server" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>文件</asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
