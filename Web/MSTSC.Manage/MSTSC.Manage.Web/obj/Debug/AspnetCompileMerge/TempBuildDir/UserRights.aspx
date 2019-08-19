<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserRights.aspx.cs" Inherits="MSTSC.Manage.Web.UserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles/bootstrapStyle/bootstrapStyle.css" type="text/css" />

    <script type="text/javascript" src="scripts/ztree/jquery.ztree.core.js"></script>
    <script type="text/javascript" src="scripts/ztree/jquery.ztree.excheck.js"></script>
    <script type="text/javascript" src="scripts/ztree/jquery.ztree.exedit.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">用户权限管理
        </li>
    </ul>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-right-5">
        <div class="panel panel-info search-panel margin-bottom-10 padding-bottom-5">
            <div class="panel-body nopadding">
                <hr class="nomargin" style="border-color: #cccccc; margin-left: 0px; margin-right: 0px;" />
                <div class="search-condition padding-5">
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
                    <h5 class="modal-title">用户权限</h5>
                    <input id="userId" type="hidden" />
                </div>
                <div class="modal-body">
                    <div class="row padding-left-20">
                        <ul id="treeData" class="ztree"></ul>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="saveRights" type="button" class="btn btn-success">
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

            $.fn.zTree.init($("#treeData"), setting, zNodes);

            $('#query').click(function () {
                $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'UserInfo.aspx/getUserList' });
            });

            $("#saveRights").click(function () {
                var rights = ",";
                var treeObj = $.fn.zTree.getZTreeObj("treeData");
                nodes = treeObj.getCheckedNodes(true);

                for (var i = 0; i < nodes.length; i++) {
                    rights += nodes[i].id + ",";
                }
                saveUserRights(rights);
                console.info(nodes);
                console.info(rights);
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
                            var opt = '';
                            if (value != '1' && !row.isDel) { opt += '<a href="javascript:void(0)" onclick="editUserRights(\'' + value + '\');return false;">设置权限</a> &nbsp;'; }
                            else if (row.isDel) {
                                opt = '<span style="color:#999;">不可操作</span>';
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

        ///////////////////////////////////

        var setting = {
            view: {
                selectedMulti: false
            },
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            edit: {
                enable: false
            }
        };

        var zNodes = [
        { id: 1, pId: 0, name: "血球仪", open: true, chkDisabled: false },
        { id: 11, pId: 1, name: "仪器列表", chkDisabled: false },
        { id: 12, pId: 1, name: "查询", chkDisabled: false },
        { id: 13, pId: 1, name: "统计", open: true, chkDisabled: false },
        { id: 131, pId: 13, name: "所有机器", chkDisabled: false },
        { id: 132, pId: 13, name: "按模式统计", chkDisabled: false },
        { id: 133, pId: 13, name: "按区域统计", chkDisabled: false },
        { id: 134, pId: 13, name: "按机型统计", chkDisabled: false },
        { id: 135, pId: 13, name: "按试剂封闭类型统计", chkDisabled: false },
        { id: 136, pId: 13, name: "按OEM统计", chkDisabled: false },

        { id: 2, pId: 0, name: "生化仪", open: true, chkDisabled: false },
        { id: 21, pId: 2, name: "仪器列表", chkDisabled: false },
        { id: 22, pId: 2, name: "查询", chkDisabled: false },
        { id: 23, pId: 2, name: "统计", open: true, chkDisabled: false },
        { id: 231, pId: 23, name: "所有机器", chkDisabled: false },
        { id: 232, pId: 23, name: "按区域统计", chkDisabled: false },
        { id: 233, pId: 23, name: "按机型统计", chkDisabled: false },

        { id: 3, pId: 0, name: "POCT", open: true, chkDisabled: false },
        { id: 31, pId: 3, name: "仪器列表", chkDisabled: false },
        { id: 32, pId: 3, name: "查询", chkDisabled: false },
        { id: 33, pId: 3, name: "统计", open: true, chkDisabled: false },
        { id: 331, pId: 33, name: "所有机器", chkDisabled: false },
        { id: 332, pId: 33, name: "按区域统计", chkDisabled: false },
        { id: 333, pId: 33, name: "按机型统计", chkDisabled: false },
        { id: 334, pId: 33, name: "总量统计", chkDisabled: false },

        { id: 8, pId: 0, name: "用户管理", open: true, chkDisabled: false },
        { id: 81, pId: 8, name: "用户列表", chkDisabled: false },
        { id: 82, pId: 8, name: "权限管理", chkDisabled: false },

        { id: 9, pId: 0, name: "日志", open: true, chkDisabled: false },
        { id: 91, pId: 9, name: "日志查询", chkDisabled: false },
        ];

        ///////////////////////////////////

        function saveUserRights(rights) {
            $.ajax({
                type: "post",
                url: "UserRights.aspx/saveUserRights",
                data: "{'id':'" + $("#userId").val() + "',"
                    + "'rights':'" + rights + "'"
                    + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert(eval(data.d));
                    if (data.d.indexOf("成功") >= 0) {
                        $('.close').click();
                        freshTable();
                    };
                },
                error: function (err) {
                    alert('保存出错.');
                }
            });
        }

        function editUserRights(id) {
            $("#userId").val(id);

            var treeObj = $.fn.zTree.getZTreeObj("treeData");
            treeObj.checkAllNodes(false);

            var select;
            var selectarry = [];
            var t = $("#treeData");
            $.ajax({
                type: "post",
                url: "UserRights.aspx/getUserRights",
                data: "{'id':'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var rights = eval(data.d);
                    if (rights) {
                        t = $.fn.zTree.init(t, setting, zNodes);
                        var zTree2 = $.fn.zTree.getZTreeObj("treeData");
                        selectarry = rights.split(",");
                        for (select = 0; select < selectarry.length; select++) {
                            zTree2.checkNode(zTree2.getNodesByParam("id", selectarry[select])[0], true);
                        }
                    }
                },
                error: function (err) {
                    console.info('查询用户权限出错.');
                }
            });

            $('#userModal').modal('show');
        }

    </script>
</asp:Content>
