<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SocketTest.aspx.cs" Inherits="WebTest.SocketTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-left: 50px;padding-top:20px;">
            <asp:Label ID="lab" runat="server" Text="" />
            <br />
            <asp:Button ID="btn" runat="server" Text="获取当前连接ID" OnClick="btn_Click" />
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div style="padding-left: 50px;">
            <asp:TextBox ID="txtId" runat="server"></asp:TextBox><asp:Label ID="labMsg" runat="server" Text="" />
            <br />
            <br />
            <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Rows="5" Columns="21"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnSend" runat="server" Text="发送消息" OnClick="btnSend_Click" />
        </div>
    </form>
</body>
</html>
