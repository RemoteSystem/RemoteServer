<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StatisticByModel.aspx.cs" Inherits="MSTSC.Manage.Web.StatisticByModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://cdn.hcharts.cn/highcharts/highcharts.js"></script>
    <script src="http://cdn.hcharts.cn/highcharts/modules/no-data-to-display.js"></script>
    <!-- 需要保存导出功能模块文件是在 highcharts.js 之后引入 -->
    <script src="http://cdn.hcharts.cn/highcharts/modules/exporting.js"></script>
    <!-- 客户端导出功能模块为可选选项 -->
    <script src="http://cdn.hcharts.cn/highcharts/modules/offline-exporting.js"></script>
    <script src="scripts/chart.js"></script>
    <!-- 表格导出 -->
    <script src="scripts/bootstrap-table/bootstrap-table-export.js"></script>
    <script src="scripts/bootstrap-table/tableExport.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">统计结果-按模式统计</li>
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
                    <button type="button" id="btnSearch" class="btn btn-default btn-normal margin-left-10">查 询</button>
                    <button type="button" id="btnExport" class="btn btn-default btn-normal margin-left-10">导出结果</button>
                </div>
            </div>
            <span class="clearfix"></span>
        </div>
    </div>

    <div>
        <div class="col-lg-8 col-md-9 col-sm-10 col-xs-12 padding-5">
            <div class="panel margin-bottom-5">
                <table id="grid"></table>
            </div>
        </div>
        <div class="col-lg-8 col-md-9 col-sm-10 col-xs-12 padding-5">
            <div id="container" style="width: 100%; height: 400px"></div>
        </div>
        <span class="clearfix"></span>
    </div>
    <script type="text/javascript">
        var $table;
        var type = 0;
        var rows = 6;

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

            getTypes();
            getSeries();
            getModels();
        });
        //初始化bootstrap-table的内容
        function InitMainTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'StatisticByModel.aspx/getDataList';
            $table = $('#grid').bootstrapTable({
                url: queryUrl,                      //请求后台的URL（*）
                method: 'POST',                      //请求方式（*）
                //toolbar: '#toolbar',              //工具按钮用哪个容器
                striped: true,                      //是否显示行间隔色
                cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: false,                   //是否显示分页（*）
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
                        return "{'conditions':'','rows':'0'}";
                    }
                    var conditions = "{\"DeviceType\":\"" + $("#selType").val()
                        + "\",\"ProductSeries\":\"" + $("#selSeries").val()
                        + "\",\"ModelType\":\"" + $("#selModel").val()
                        + "\"}";

                    var temp = "{"
                    + "'conditions':'" + conditions + "'"
                    + ",'rows':'" + rows         //页面大小                   
                    + "'}";
                    return temp;
                },
                responseHandler: function (res) {
                    //在ajax请求成功后，填充数据之前可以对返回的数据进行处理  
                    return JSON.parse(res.d);
                },
                columns: [
                    {
                        field: 'key',
                        title: '分析模式',
                        align: 'center',
                        width: '60%'
                    }, {
                        field: 'value',
                        title: '样本数',
                        align: 'center',
                        width: '40%'
                    }],
                onLoadSuccess: function (data) {
                    if (type != 0) {
                        refreshPie(data);
                    }
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
            $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'StatisticByModel.aspx/getDataList' });
            //$table.bootstrapTable('refresh', { url: 'StatisticByModel.aspx/getDataList' });
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

        function refreshPie(data) {
            var chart = $('#container').highcharts();
            if (!chart) {
                initPie('container', '模式-样本数');
                chart = $('#container').highcharts();
            }
            var datas = new Array();
            if (data) {
                var arr = new Array();
                for (var i = 0; i < data.length; i++) {
                    datas[i] = new Array(data[i]["key"], parseInt(data[i]["value"]));
                }
                //datas = [['全血-CBC', 25], ['全血-CBC+CRP', 29], ['全血-CRP', 34], ['预稀释-CBC', 16], ['预稀释-CBC+CRP', 28], ['预稀释-CRP', 41]];
            }
            chart.series[0].setData(datas);
            chart.redraw();
        }

        function exportExcel() {
            $("#grid").tableExport({
                type: "excel", escape: "true", fileName: "统计结果-按模式统计", noNumricColumns: [0]
            });
        }
    </script>
</asp:Content>
