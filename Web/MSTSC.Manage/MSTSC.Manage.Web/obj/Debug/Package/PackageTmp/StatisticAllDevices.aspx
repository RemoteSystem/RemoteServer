<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StatisticAllDevices.aspx.cs" Inherits="MSTSC.Manage.Web.StatisticAllDevices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/bootstrap-table/bootstrap-table-export.js"></script>
    <script src="scripts/bootstrap-table/tableExport.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">统计结果-所有机器</li>
    </ul>
    <div class="panel panel-info margin-5 padding-10">
        <div class="panel-body nopadding">
            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 padding-5">
                <div class="form-inline">
                    <span>产品类型</span>
                    <select id="selType" class="form-control" style="min-width: 160px;">
                        <option value="0">请选择</option>
                        <option value="血液细胞分析仪">血液细胞分析仪</option>
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
                    <button type="button" id="btnSearch" class="btn btn-default btn-normal">查 询</button>
                    <button type="button" id="btnExport" class="btn btn-default btn-normal margin-left-10">导出结果</button>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 col-xs-12 padding-5 form-inline">
                    <button type="button" id="btnExportyb" class="btn btn-default btn-normal">导出样本</button>
                    <button type="button" id="btnExportcw" class="btn btn-default btn-normal margin-left-10">导出错误</button>
            </div>            
        </div>
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

            $("#btnSearch").click(function () {
                freshTable();
            });

            $("#btnExport").click(function () {
                exportExcel();
            });

            $('#btnExportyb').click(function () {
                window.open("Export.ashx?Action=yangben","_blank");
            });

            $('#btnExportcw').click(function () {
                window.open("Export.ashx?Action=cuowu", "_blank");
            });

            getTypes();
            getSeries();
            getModels();
        });
        //初始化bootstrap-table的内容
        function InitMainTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'StatisticAllDevices.aspx/getDeviceList';
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
                uniqueId: "SN",                     //每一行的唯一标识，一般为主键列
                //showToggle: true,                   //是否显示详细视图和列表视图的切换按钮
                cardView: false,                    //是否显示详细视图
                detailView: false,                  //是否显示父子表
                exportTypes: ['excel'],             //导出文件类型 
                //得到查询的参数
                queryParams: function (params) {
                    //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    if (type == 0) {
                        return "{'conditions':'','rows':'0','page':'0','sort':'','sortOrder':''}";
                    }
                    var conditions = "{\"DeviceType\":\"" + $("#selType").val()
                        + "\",\"ProductSeries\":\"" + $("#selSeries").val()
                        + "\",\"ModelType\":\"" + $("#selModel").val()
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
                        field: 'count_times_total',
                        title: '样本数',
                        align: 'center'
                    }, {
                        field: 'reagent_dil',
                        title: '消耗稀释液',
                        align: 'center'
                    }, {
                        field: 'reagent_lh',
                        title: '消耗溶血剂',
                        align: 'center'
                    }, {
                        field: 'reagent_r2',
                        title: '消耗CRP R2',
                        align: 'center'
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
            $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'StatisticAllDevices.aspx/getDeviceList' });
            //$table.bootstrapTable('refresh', { url: 'StatisticAllDevices.aspx/getDeviceList' });
        }

        function getTypes() {
            $.ajax({
                type: "post",
                url: "DeviceQuery.aspx/getProductType",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval(data.d);

                    var opts = "<option value=\"0\">请选择</option>";
                    for (res in result) {
                        opts += "<option value=\"" + result[res]['key'] + "\">" + result[res]['value'] + "</option>";
                    }
                    $("#selType").html(opts);
                },
                error: function (err) {
                }
            });
        }
        function getSeries() {
            $.ajax({
                type: "post",
                url: "DeviceQuery.aspx/getProductSeries",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval(data.d);

                    var opts = "<option value=\"0\">请选择</option>";
                    for (res in result) {
                        opts += "<option value=\"" + result[res]['key'] + "\">" + result[res]['value'] + "</option>";
                    }
                    $("#selSeries").html(opts);
                },
                error: function (err) {
                }
            });
        }
        function getModels() {
            $.ajax({
                type: "post",
                url: "DeviceQuery.aspx/getProductModel",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval(data.d);

                    var opts = "<option value=\"0\">请选择</option>";
                    for (res in result) {
                        opts += "<option value=\"" + result[res]['key'] + "\">" + result[res]['value'] + "</option>";
                    }
                    $("#selModel").html(opts);
                },
                error: function (err) {
                }
            });
        }

        function exportExcel() {
            $("#grid").tableExport({
                type: "excel", escape: "true", fileName: "统计结果-所有机器", noNumricColumns: [0, 1, 2]
            });
        }
    </script>
</asp:Content>
