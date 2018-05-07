<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StatisticAllDevices.aspx.cs" Inherits="MSTSC.Manage.Web.StatisticAllDevices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">统计结果-所有机器</li>
    </ul>
    <div class="panel panel-info margin-5 padding-10">
        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
            <div class="form-inline">
                <span>产品类型</span>
                <select id="selType" class="form-control" style="min-width: 160px;">
                    <option value="0">请选择</option>
                    <option value="1">血液分析仪</option>
                    <option value="2">其他</option>
                </select>
            </div>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
            <div class="form-inline">
                <span>产品系列</span>
                <select id="selSeries" class="form-control" style="min-width: 160px;">
                    <option value="0">请选择</option>
                    <option value="3diff">三分类</option>
                    <option value="5diff">五分类</option>
                </select>
            </div>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
            <div class="form-inline">
                <span>产品项目</span>
                <select id="selModel" class="form-control" style="min-width: 160px;">
                    <option value="0">请选择</option>
                </select>
            </div>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
            <div class="form-inline">
                <span>统计条件</span>
                <select id="selCondition" class="form-control" style="min-width: 160px;">
                    <option value="0">所有机器</option>
                </select>
            </div>
        </div>
        <span class="clearfix"></span>
    </div>

    <div class="panel padding-left-5 padding-right-5">
        <table id="grid"></table>
    </div>

    <script type="text/javascript">
        var $table;
        var type = 0;
        var rows = 10;
        var page = 1;
        var sort = "";
        var order = "";

        $(document).ready(function () {
            InitMainTable();

            $("#selType").change(function () {
                freshTable();
            });
            $("#selSeries").change(function () {
                freshTable();
            });
            $("#selModel").change(function () {
                freshTable();
            });
        });
        //初始化bootstrap-table的内容
        function InitMainTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'DeviceQuery.aspx/getDeviceList';
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
                    var conditions = "{\"QueryRange\":\""
                        + "\",\"DeviceType\":\""
                        + "\",\"QueryText\":\""
                        + "\",\"ProductSeries\":\""
                        + "\",\"ModelType\":\""
                        + "\",\"OEM\":\""
                        + "\",\"Agent\":\""
                        + "\",\"ReagentType\":\""
                        + "\",\"Region\":\""
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
                    //在ajax请求成功后，填充数据之前可以对返回的数据进行处理  
                    return JSON.parse(res.d);
                },
                columns: [
                    {
                        field: 'DeviceName',
                        title: '机器名'
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
                        title: '样本数'
                    }, {
                        field: 'ProductModel',
                        title: '消耗稀释液'
                    }, {
                        field: 'SESSION_ID',
                        title: '消耗溶血剂'
                    }, {
                        field: 'SESSION_ID',
                        title: '消耗CRP R2'
                    }],
                onLoadSuccess: function () {
                },
                onLoadError: function () {
                    alert("数据加载失败！");
                },
                onDblClickRow: function (row, $element) {
                    $("#grid tbody tr").css("background-color", "");
                    $element.css("background-color", "#C0C0C0");
                }
            });
        };

        function freshTable() {
            type = 1;
            $table.bootstrapTable('refresh', { url: 'DeviceQuery.aspx/getDeviceList' });
        }

        function getTypes() { }
        function getSeries() { }
        function getModels() { }
    </script>
</asp:Content>
