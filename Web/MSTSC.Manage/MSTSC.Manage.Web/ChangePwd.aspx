<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="MSTSC.Manage.Web.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">修改密码
        </li>
    </ul>
    <div class="row">
        <div class="col-lg-12 padding-20">
            <label style="width: 100px; text-align: right">原密码：</label>
            <input id="oldpwd" type="password" class="margin-left-10 padding-left-10" style="width: 180px;" />
        </div>
        <div class="col-lg-12 padding-20">
            <label style="width: 100px; text-align: right">新密码：</label>
            <input id="newpwd" type="password" class="margin-left-10 padding-left-10" style="width: 180px;" />
        </div>
        <div class="col-lg-12 padding-20">
            <label style="width: 100px; text-align: right">确认密码：</label>
            <input id="confirmpwd" type="password" class="margin-left-10 padding-left-10" style="width: 180px;" />
        </div>

        <div class="col-lg-4 col-md-6 col-sm-6 padding-20 text-center">
            <button id="save" type="button" class="btn btn-success">
                保存
            </button>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#save").click(function () { resetpwd(); });
        });

        function resetpwd(id) {
            if (!$("#oldpwd").val() || !$("#newpwd").val()) {
                alert("请输入密码");
                return;
            }
            if ($("#newpwd").val() === $("#confirmpwd").val()) {
                $.ajax({
                    type: "post",
                    url: "UserInfo.aspx/changePwd",
                    data: "{'oldPwd':'" + $("#oldpwd").val() + "','newPwd':'" + $("#newpwd").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        alert(eval(data.d));
                    },
                    error: function (err) {
                        console.info('修改用户密码出错.');
                    }
                });
            } else {
                alert("两次输入密码不一致.");
            }
        }

    </script>
</asp:Content>
