<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MSTSC.Manage.UI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录页</title>
    <style type="text/css" title="currentStyle" media="screen" mce_bogus="1">
        #divcenter {
            position: absolute; /*层漂浮*/
            top: 50%;
            left: 50%;
            width: 450px;
            height: 280px;
            padding: 10px;
            margin-top: -200px; /*注意这里必须是DIV高度的一半*/
            margin-left: -250px; /*这里是DIV宽度的一半*/
            border: 2px solid #bbbbbb;
            background: #eeeeee;
        }

        * {
            font-family: Arial;
        }

        .validate {
            color: Red;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divcenter">
            <table cellpadding="5" cellspacing="0" style="margin-top: 30px; font-size: larger;">
                <tr>
                    <td colspan="3" style="padding-bottom: 20px; text-align: center;">
                        <strong style="font-size: 20px;">用户登录</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px; text-align: right">
                        <strong style="font-size: 15px;">用户名:</strong>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_LoginName" Width="200px" Height="22px" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator CssClass="validate" ID="RequiredFieldValidator11" ErrorMessage="请输入用户名."
                            ControlToValidate="txt_LoginName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <div></div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <strong style="font-size: 15px;">密码:</strong>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_Password" Width="200px" Height="22px" TextMode="Password"
                            AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validate" ErrorMessage="请输入密码."
                            ControlToValidate="txt_Password" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="2" style="text-align: left">
                        <div id="LoginFail" runat="server" style="color: Red; font-size: small;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Button ID="btn_Login" runat="server" style="margin-left: 20px; font-size: 11.5pt; padding: 3px 10px;" Text="登录" OnClick="btn_Login_Click" />
                        <asp:Button ID="btn_Reset" runat="server" style="margin-left: 20px; font-size: 11.5pt; padding: 3px 10px;" Text="重置" OnClick="btn_Reset_Click" />                        
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
