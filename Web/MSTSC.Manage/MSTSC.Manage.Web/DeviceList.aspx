<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DeviceList.aspx.cs" Inherits="MSTSC.Manage.Web.DeviceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb margin-bottom-5">
        <li class="active">仪器列表</li>
    </ul>
    <div class="col-lg-6 col-md-6 col-sm-11 col-xs-12">
        <div class="panel panel-info search-panel margin-bottom-10 padding-bottom-10">
            <%--<div class="quick-search-condition padding-5">
                <div class="form-inline">
                    <label>产品类型</label>
                    <select id="quickptype" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0">请选择</option>
                        <option value="1">血球分析仪</option>
                        <option value="2">其他</option>
                    </select>
                    <input type="text" id="querytext" class="form-control margin-top-5 margin-bottom-5" value="" placeholder="请输入查询条件" />
                    <button type="button" id="quickquery" class="btn btn-default margin-top-5 margin-bottom-5 margin-left-10">快速查询</button>
                </div>
            </div>
            <hr class="nomargin" style="border-color: #cccccc;" />--%>
            <div class="search-condition padding-5">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                    <label>产品类型</label>
                    <select id="ptype" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0">请选择</option>
                        <option value="1">血球分析仪</option>
                    </select>
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                    <label>产品型号</label>
                    <select id="pmodel" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0">请选择</option>
                    </select>
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                    <label>OEM代号</label>
                    <select id="oem" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0">请选择</option>
                    </select>
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                    <label>代理商代号</label>
                    <select id="agent" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0">请选择</option>
                    </select>
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                    <label>封闭试剂类型</label>
                    <select id="reagenttype" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0">请选择</option>
                        <option value="open">开放</option>
                        <option value="close">封闭</option>
                    </select>
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                    <label>装机区域</label>
                    <select id="region" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0">请选择</option>
                    </select>
                </div>

                <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12 col-lg-offset-2 col-md-offset-2 nopadding form-inline">
                    <div class="radio margin-left-20">
                        <label>
                            <input type="radio" name="rdoconnect" id="optionsRadios1" value="0" checked="checked" />所有仪器
                        </label>
                    </div>
                    <div class="radio margin-left-20">
                        <label>
                            <input type="radio" name="rdoconnect" id="optionsRadios2" value="1" />已连接仪器
                        </label>
                    </div>
                    <div class="radio margin-left-20">
                        <label>
                            <input type="radio" name="rdoconnect" id="optionsRadios3" value="2" />未连接仪器
                        </label>
                    </div>
                    <button type="button" id="query" class="btn btn-default margin-top-5 margin-bottom-5 margin-left-20">查询</button>
                </div>
            </div>
            <span class="clearfix"></span>
        </div>

        <div class="panel">
            <table id="grid"></table>
        </div>
    </div>
    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
        <div class="row">
        </div>
    </div>

    <script type="text/javascript">
        var $table;
        var type = 0;
        var rows = 10;
        $(document).ready(function () {
            InitMainTable();

            var opts = "<option value=\"0\">请选择</option>";
            for (var i = 1; i < 100; i++) {
                opts += "<option value=\"" + i + "\">" + i + "</option>";
            }
            $("#oem").html(opts);
            $("#agent").html(opts);

            //$('#quickquery').click(function () {
            //    type = 0;
            //    $table.bootstrapTable('refresh', { url: 'DeviceList.aspx/getDeviceList' });
            //});

            $('#query').click(function () {
                type = 2;
                $table.bootstrapTable('refresh', { url: 'DeviceList.aspx/getDeviceList' });
            });
        });
        //初始化bootstrap-table的内容
        function InitMainTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'DeviceList.aspx/getDeviceList';
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
                pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
                search: false,                      //是否显示表格搜索
                strictSearch: true,
                //showColumns: true,                  //是否显示所有的列（选择显示的列）
                //showRefresh: true,                  //是否显示刷新按钮
                minimumCountColumns: 2,             //最少允许的列数
                clickToSelect: true,                //是否启用点击选中行
                //height: 500,                      //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                uniqueId: "SN",                     //每一行的唯一标识，一般为主键列
                //showToggle: true,                   //是否显示详细视图和列表视图的切换按钮
                cardView: false,                    //是否显示详细视图
                detailView: false,                  //是否显示父子表
                //得到查询的参数
                queryParams: function (params) {
                    //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    if (type == 0) {
                        return "{'conditions':'','rows':'0','page':'0','sort':'','sortOrder':''}";
                    }
                    var conditions = "{\"QueryRange\":\"" + $('input[name="rdoconnect"]:checked').val()
                        + "\",\"DeviceType\":\"" + $("#ptype").val()
                        + "\",\"ProductSeries\":\"" + $("#ptype").val()
                        + "\",\"ModelType\":\"" + $("#pmodel").val()
                        + "\",\"OEM\":\"" + $("#oem").val()
                        + "\",\"Agent\":\"" + $("#agent").val()
                        + "\",\"ReagentType\":\"" + $("#reagenttype").val()
                        + "\",\"Region\":\"" + $("#region").val()
                        + "\"}";

                    var temp = "{"
                    + "'conditions':'" + conditions + "'"
                    + ",'rows':" + (params.limit ? params.limit : rows)                        //页面大小
                    + ",'page':" + (params.limit ? (params.offset / params.limit) + 1 : 1)   //页码
                    + ",'sort':'" + (params.sort ? params.sort : "")      //排序列名
                    + "','sortOrder':'" + params.order //排位命令（desc，asc）
                    + "'}";
                    return temp;
                },
                responseHandler: function (res) {
                    //在ajax请求成功后，填充数据之前可以对返回的数据进行处理  
                    return JSON.parse(res.d);
                },
                columns: [
                    {
                        field: 'DeviceName',
                        title: '仪器名称'
                    }, {
                        field: 'SIM',
                        title: 'SIM卡号',
                        sortable: true
                    }, {
                        field: 'SN',
                        title: '仪器序列号',
                        sortable: true
                    }, {
                        field: 'ProductSeries',
                        title: '产品类型'
                    }, {
                        field: 'ProductModel',
                        title: '产品型号'
                    }, {
                        field: 'SESSION_ID',
                        title: '状态',
                        formatter: function (value, row, index) {
                            var div = "<div style='width:45px;'>" + value + "</div>";
                            return div;
                        }
                    }],
                onLoadSuccess: function () {
                },
                onLoadError: function () {
                    alert("数据加载失败！");
                },
                onDblClickRow: function (row, $element) {
                    alert("SN是:" + row.SN);
                    $("#grid tbody tr").css("background-color", "");
                    $element.css("background-color", "#C0C0C0");
                }
            });
        };
    </script>
</asp:Content>
