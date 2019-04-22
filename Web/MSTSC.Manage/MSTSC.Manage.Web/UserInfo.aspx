<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="MSTSC.Manage.Web.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">用户管理列表
        </li>
    </ul>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-right-5">
        <div class="panel panel-info search-panel margin-bottom-10 padding-bottom-5">
            <div class="panel-body nopadding">
                <hr class="nomargin" style="border-color: #cccccc; margin-left: 0px; margin-right: 0px;" />
                <div class="search-condition padding-5">
                    <div class="nopadding form-inline">
                        <div class="padding-5">
                            <button type="button" id="addUser" class="btn btn-success">新增用户</button>
                        </div>
                    </div>
                    <div class="nopadding form-inline">
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
                            <div class="form-inline">
                                <span>姓名</span>
                                <input id="name" />
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
                            <div class="form-inline">
                                <span>性别</span>
                                <select id="selSex" class="form-control" style="min-width: 160px;">
                                    <option value="">请选择</option>
                                    <option value="1">男</option>
                                    <option value="2">女</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
                            <div class="form-inline">
                                <span>是否删除</span>
                                <select id="selDel" class="form-control" style="min-width: 160px;">
                                    <option value="">请选择</option>
                                    <option value="0">否</option>
                                    <option value="1">是</option>
                                </select>
                            </div>
                        </div>
                        <button type="button" id="query" class="btn btn-default margin-top-5 margin-bottom-5 margin-left-10">查询</button>
                    </div>
                </div>
                <span class="clearfix"></span>
            </div>
        </div>

        <div class="panel">
            <table id="grid"></table>
        </div>
    </div>

    <div class="modal fade" id="userModal" tabindex="-1" role="dialog" aria-labelledby="userModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×
                    </button>
                    <h5 class="modal-title">新增用户</h5>
                    <input id="userId" type="hidden" />
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 padding-20">
                            <label style="width: 100px; text-align: right">用户名：</label>
                            <input id="userName" type="text" class="margin-left-10 padding-left-10" style="width: 180px;" />
                        </div>
                        <div class="col-lg-12 padding-20">
                            <label style="width: 100px; text-align: right">姓名：</label>
                            <input id="realname" type="text" class="margin-left-10 padding-left-10" style="width: 180px;" />
                        </div>
                        <div class="col-lg-12 padding-20 form-inline">
                            <label style="width: 100px; text-align: right">性别：</label>
                            <select id="sex" class="form-control margin-left-10" style="width: 180px;">
                                <option value="1">男</option>
                                <option value="2">女</option>
                            </select>
                        </div>
                        <div class="col-lg-12 padding-20">
                            <label style="width: 100px; text-align: right">年龄：</label>
                            <input id="age" type="text" class="margin-left-10 padding-left-10" style="width: 180px;" />
                        </div>
                        <div class="col-lg-12 padding-20 form-inline">
                            <label style="width: 100px; text-align: right">管理员：</label>
                            <select id="isAdmin" class="form-control margin-left-10" style="width: 180px;">
                                <option value="0">否</option>
                                <option value="1">是</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="saveUser" type="button" class="btn btn-success">
                        保存
                    </button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        关闭
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var $table;
        var rows = 10;
        var page = 1;
        var sort = "id";
        var order = "";

        $(document).ready(function () {
            InitMainTable();

            $('#query').click(function () {
                $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'UserInfo.aspx/getUserList' });
            });

            $('#addUser').click(function () {
                $("#userId").val("");

                $("#userName").removeAttr("disabled");
                $("#userName").val("");
                $("#realname").val("");
                $("#sex").val(1);
                $("#age").val("");
                $("#isAdmin").val(0);

                $('#userModal').modal('show');
            });

            $("#saveUser").click(function () {
                saveUser();
            });

        });
        //初始化bootstrap-table的内容
        function InitMainTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'UserInfo.aspx/getUserList';
            $table = $('#grid').bootstrapTable({
                url: queryUrl,                      //请求后台的URL（*）
                method: 'POST',                      //请求方式（*）
                //toolbar: '#toolbar',              //工具按钮用哪个容器
                striped: true,                      //是否显示行间隔色
                cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: true,                   //是否显示分页（*）
                sortable: true,                     //是否启用排序
                sortOrder: "asc",                   //排序方式
                sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                pageNumber: 1,                      //初始化加载第一页，默认第一页,并记录
                pageSize: rows,                     //每页的记录行数（*）
                pageList: [],                       //可供选择的每页的行数（*）
                search: false,                      //是否显示表格搜索
                strictSearch: true,
                //showColumns: true,                  //是否显示所有的列（选择显示的列）
                //showRefresh: true,                  //是否显示刷新按钮
                minimumCountColumns: 2,             //最少允许的列数
                clickToSelect: true,                //是否启用点击选中行
                //height: 500,                      //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                uniqueId: "id",                     //每一行的唯一标识，一般为主键列
                //showToggle: true,                   //是否显示详细视图和列表视图的切换按钮
                cardView: false,                    //是否显示详细视图
                detailView: false,                  //是否显示父子表
                //得到查询的参数
                queryParams: function (params) {
                    var conditions = "{\"name\":\"" + $("#name").val()
                        + "\",\"sex\":\"" + $("#selSex").val()
                        + "\",\"isDel\":\"" + $("#selDel").val()
                        + "\"}";

                    rows = params.limit ? params.limit : rows;
                    page = params.limit ? (params.offset / params.limit) + 1 : page;
                    sort = params.sort ? params.sort : sort;
                    order = params.order ? params.order : order;

                    var temp = "{"
                    + "'conditions':'" + conditions + "'"
                    + ",'rows':" + rows         //页面大小
                    + ",'page':" + page         //页码
                    + ",'sort':'" + sort        //排序列名
                    + "','sortOrder':'" + order //排位命令（desc，asc）
                    + "'}";
                    return temp;
                },
                responseHandler: function (res) {
                    return JSON.parse(res.d);
                },
                columns: [
                    {
                        field: 'name',
                        title: '姓名'
                    }, {
                        field: 'userName',
                        title: '用户名'
                    }, {
                        field: 'sex',
                        title: '性别',
                        formatter: function (value, row, index) {
                            var val = (value == "1" ? "男" : (value == "2" ? "女" : ""));
                            return val;
                        }
                    }, {
                        field: 'age',
                        title: '年龄'
                    }, {
                        field: 'isAdmin',
                        title: '管理员',
                        formatter: function (value, row, index) {
                            var val = (value == "1" ? "是" : "否");
                            return val;
                        }
                    }, {
                        field: 'isDel',
                        title: '有效',
                        formatter: function (value, row, index) {
                            var val = (value == "1" ? "已删除" : "有效");
                            return val;
                        }
                    }, {
                        field: 'id',
                        title: '操作',
                        width: 200,
                        formatter: function (value, row, index) {
                            var opt = ' <a href="javascript:void(0)" onclick="edituser(\'' + value + '\');return false;">编辑</a> &nbsp;';
                            opt += ' <a href="javascript:void(0)" onclick="resetpwd(\'' + value + '\');return false;">重置密码</a> &nbsp;';
                            if (value == '1') { opt += ''; }
                            else if (row["isDel"]) {
                                opt += ' <a href="javascript:void(0)" onclick="enableuser(\'' + value + '\');return false;">启用</a>';
                            } else {
                                opt += ' <a href="javascript:void(0)" onclick="deluser(\'' + value + '\');return false;">删除</a>';
                            }
                            return opt;
                        }
                    }],
                onLoadSuccess: function () {
                },
                onLoadError: function () {
                    alert("数据加载失败！");
                },
                onSort: function (name, order) {
                },
                onPageChange: function (name, order) {
                },
                onDblClickRow: function (row, $element) {
                    $("#grid tbody tr").css("background-color", "");
                    $element.css("background-color", "#C0C0C0");
                }
            });
        };

        function freshTable() {
            $table.bootstrapTable('refreshOptions', { url: 'UserInfo.aspx/getUserList' });
        }

        function edituser(id) {
            $("#userId").val(id);
            $.ajax({
                type: "post",
                url: "UserInfo.aspx/getUser",
                data: "{'id':'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval("[" + data.d + "]")[0];
                    $("#userName").attr("disabled", "disabled");
                    $("#userName").val(result["userName"]);
                    $("#realname").val(result["name"]);
                    $("#sex").val(result["sex"]);
                    $("#age").val(result["age"]);
                    $("#isAdmin").val(result["isAdmin"] == "True" ? 1 : 0);
                },
                error: function (err) {
                    console.info('获取用户出错.');
                }
            });
            $('#userModal').modal('show');
        }

        function saveUser() {
            $.ajax({
                type: "post",
                url: "UserInfo.aspx/saveUser",
                data: "{'id':'" + $("#userId").val() + "',"
                    + "'userName':'" + $("#userName").val() + "',"
                    + "'name':'" + $("#realname").val() + "',"
                    + "'sex':'" + $("#sex").val() + "',"
                    + "'age':'" + $("#age").val() + "',"
                    + "'isAdmin':'" + $("#isAdmin").val() + "'"
                    + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert(eval(data.d));
                    if (data.d.indexOf("成功") >= 0) $('#query').click();
                },
                error: function (err) {
                    alert('保存出错.');
                }
            });
        }

        function deluser(id) {
            var r = confirm("确定要删除用户吗?")
            if (r == true) {
                $.ajax({
                    type: "post",
                    url: "UserInfo.aspx/delUser",
                    data: "{'id':'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('#query').click();
                    },
                    error: function (err) {
                        console.info('删除用户出错.');
                    }
                });
            }
        }

        function resetpwd(id) {
            var r = confirm("确定要重置用户密码吗?")
            if (r == true) {
                $.ajax({
                    type: "post",
                    url: "UserInfo.aspx/resetPwd",
                    data: "{'id':'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        alert(eval(data.d));
                    },
                    error: function (err) {
                        console.info('重置用户密码出错.');
                    }
                });
            }
        }

        function enableuser(id) {
            $.ajax({
                type: "post",
                url: "UserInfo.aspx/enableUser",
                data: "{'id':'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert(eval(data.d));
                    $('#query').click();
                },
                error: function (err) {
                    console.info('启用用户出错.');
                }
            });
        }

    </script>
</asp:Content>
