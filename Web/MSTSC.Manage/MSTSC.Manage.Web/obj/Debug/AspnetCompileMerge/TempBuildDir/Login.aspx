<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MSTSC.Manage.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <title>用户登录-远程管理系统</title>
    <link href="styles/font-awesome.min.css" rel="stylesheet" />
    <!--bootstrap库-->
    <link href="styles/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="styles/login.css" rel="stylesheet" />
</head>
<body onkeydown="keyLogin();">
    <div class="container">
        <div class="row">
            <div class="form-horizontal">
                <div class="well well-sm" style="background-color: white; padding-bottom: 20px;">
                    <span class="heading">用户登录</span>
                    <div class="form-group">
                        <input type="text" class="form-control" id="username" placeholder="请输入用户名" />
                        <i class="fa fa-user"></i>
                    </div>
                    <div class="form-group">
                        <input type="password" class="form-control" id="password" placeholder="请输入密码" />
                        <i class="fa fa-lock"></i>
                    </div>
                    <div id="logininfo" style="color: red;">&nbsp;</div>
                    <div class="form-group">
                        <%--<div class="main-checkbox">
                            <input type="checkbox" value="None" id="checkbox1" name="check" />
                            <label for="checkbox1"></label>
                        </div>
                        <span class="text">Remember me</span>--%>
                        <button id="btnlogin" class="btn btn-default">登  录</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>

<script src="scripts/jquery-3.0.0.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnlogin').click(function () {
            var username = $("#username").val();
            var password = $("#password").val();
            if (username && password) {
                $("#logininfo").html('&nbsp;');
                $.ajax({
                    type: "post",
                    url: "Login.aspx/UserLogin",
                    data: "{'username':'" + username + "','password':'" + password + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        if (res == '0') {
                            window.location.href = "Index.aspx";
                        } else {
                            $("#logininfo").html('用户名或密码错误.');
                        }
                    },
                    error: function (err) {
                    }
                });
            } else {
                $("#logininfo").html('请输入用户名和密码');
            }
        });
    });

    function keyLogin() {
        if (event.keyCode == 13) {
            $('#btnlogin').click();
        }
    }
</script>
</html>
