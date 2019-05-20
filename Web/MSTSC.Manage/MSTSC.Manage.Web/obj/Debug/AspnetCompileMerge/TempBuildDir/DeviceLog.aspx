﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DeviceLog.aspx.cs" Inherits="MSTSC.Manage.Web.DeviceLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/bootstrap-datetimepicker/bootstrap-datetimepicker.css" rel="stylesheet" />
    <script src="scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.js"></script>
    <script src="scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.zh-CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">日志查询</li>
    </ul>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-right-10">
        <div class="panel panel-info search-panel margin-bottom-10 padding-bottom-5">
            <div class="panel-body nopadding">
                <hr class="nomargin" style="border-color: #cccccc; margin-left: 0px; margin-right: 0px;" />
                <div class="search-condition padding-5">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 nopadding form-inline">
                        <label>仪器类型</label>
                        <select id="selType" class="form-control margin-top-5 margin-bottom-5">
                            <option value="生化仪">生化仪</option>
                        </select>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 nopadding form-inline">
                        <label>仪器型号</label>
                        <select id="selModel" class="form-control margin-top-5 margin-bottom-5">
                            <option value="0">请选择</option>
                        </select>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 nopadding form-inline">
                        <label>装机区域</label>
                        <select id="selRegion" class="form-control margin-top-5 margin-bottom-5">
                            <option value="0">请选择</option>
                        </select>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 nopadding form-inline">
                        <label>仪器名称</label>
                        <input id="DeviceName" class="form-control margin-top-5 margin-bottom-5" />
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 nopadding form-inline">
                        <label>SIM卡号</label>
                        <input id="SIM" class="form-control margin-top-5 margin-bottom-5" />
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 nopadding form-inline">
                        <label>仪器序列号</label>
                        <input id="SN" class="form-control margin-top-5 margin-bottom-5" />
                    </div>

                    <div class="col-lg-5 col-md-6 col-sm-10 col-xs-12 nopadding form-inline">
                        <label>发生时间</label>
                        <input id="dtStart" placeholder="开始时间" class="form-control margin-top-5 margin-bottom-5" style="width: 140px !important;" />
                        <label style="min-width: 5px;">-</label>
                        <input id="dtEnd" placeholder="结束时间" class="form-control margin-top-5 margin-bottom-5" style="width: 140px !important;" />
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-10 col-xs-12 nopadding form-inline">
                        <button type="button" id="query" class="btn btn-default btn-normal margin-top-5 margin-bottom-5 margin-left-20">查 询</button>
                        <button type="button" id="export" class="btn btn-default btn-normal margin-top-5 margin-bottom-5 margin-left-20">导 出</button>
                    </div>
                </div>
                <span class="clearfix"></span>
            </div>
        </div>

        <div class="panel">
            <table id="grid"></table>
        </div>
    </div>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×
                    </button>
                    <h5 class="modal-title">日志详情</h5>
                </div>
                <div class="modal-body">
                    <div id="div_content" class="row" style="padding:10px; overflow:auto;">                        
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        关闭
                    </button>
                </div>
            </div>
        </div>
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
            InitDateTimePicker();

            $('#query').click(function () {
                type = 2;
                var page = 1;
                var sort = "";
                var order = "";

                $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'DeviceLog.aspx/getLogList' });
            });

            $('#export').click(function () {
                var conditions = "{\"DeviceType\":\"生化仪"
                        + "\",\"Model\":\"" + $("#selModel").val()
                        + "\",\"Region\":\"" + $("#selRegion").val()
                        + "\",\"DeviceName\":\"" + $("#DeviceName").val()
                        + "\",\"SIM\":\"" + $("#SIM").val()
                        + "\",\"SN\":\"" + $("#SN").val()
                        + "\",\"dtStart\":\"" + $("#dtStart").val()
                        + "\",\"dtEnd\":\"" + $("#dtEnd").val()
                        + "\"}";
                window.open("Export.ashx?Action=bio_log&conditions=" + conditions, "_blank");
            });

            getModels();
            getRegion();
        });
        //初始化bootstrap-table的内容
        function InitMainTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'DeviceLog.aspx/getLogList';
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
                //得到查询的参数
                queryParams: function (params) {
                    //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    if (type == 0) {
                        return "{'conditions':'','rows':'0','page':'0','sort':'','sortOrder':''}";
                    }
                    var conditions = "{\"DeviceType\":\"生化仪"
                        + "\",\"Model\":\"" + $("#selModel").val()
                        + "\",\"Region\":\"" + $("#selRegion").val()
                        + "\",\"DeviceName\":\"" + $("#DeviceName").val()
                        + "\",\"SIM\":\"" + $("#SIM").val()
                        + "\",\"SN\":\"" + $("#SN").val()
                        + "\",\"dtStart\":\"" + $("#dtStart").val()
                        + "\",\"dtEnd\":\"" + $("#dtEnd").val()
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
                    var json = JSON.parse(res.d);
                    return json;
                },
                columns: [
                    {
                        field: 'SN',
                        title: '序号',
                        width: 40,
                        align: 'center',
                        formatter: function (value, row, index) {
                            return (page - 1) * rows + index + 1;
                        }
                    }, {
                        field: 'DeviceName',
                        title: '仪器名称'
                    }, {
                        field: 'SIM',
                        title: 'SIM卡号',
                        align: 'center'
                    }, {
                        field: 'SN',
                        title: '仪器序列号',
                        align: 'center'
                    }, {
                        field: 'DeviceType',
                        title: '仪器类型',
                        align: 'center'
                    }, {
                        field: 'Model',
                        title: '仪器型号',
                        align: 'center'
                    }, {
                        field: 'dtinsert',
                        title: '发生时间',
                        align: 'center',
                        formatter: function (value, row, index) {
                            return value.replace("T", " ");
                        }
                    }, {
                        field: 'content',
                        title: '日志内容',
                        align: 'center',
                        formatter: function (value, row, index) {
                            return "<label class='aa' title='点击查看详情' val='" + value + "' onclick=viewLog(this)>上报信息</label>";
                        }
                    }],
                onLoadSuccess: function () {

                },
                onLoadError: function () {
                },
                onSort: function (name, order) {
                },
                onPageChange: function (name, order) {
                },
                onDblClickRow: function (row, $element) {
                }
            });
        }

        function freshTable() {
            $table.bootstrapTable('refreshOptions', { url: 'DeviceLog.aspx/getLogList' });
        }

        function getModels() {
            $.ajax({
                type: "post",
                url: "BioDeviceQuery.aspx/getModel",
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

        function getRegion() {
            $.ajax({
                type: "post",
                url: "BioDeviceQuery.aspx/getRegion",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval(data.d);

                    var opts = "<option value=\"0\">请选择</option>";
                    for (res in result) {
                        opts += "<option value=\"" + result[res]['key'] + "\">" + result[res]['value'] + "</option>";
                    }
                    $("#selRegion").html(opts);
                },
                error: function (err) {
                }
            });
        }

        function InitDateTimePicker() {
            $("#dtStart").datetimepicker({
                format: 'yyyy-mm-dd hh:ii',  //格式  如果只有yyyy-mm-dd那就是0000-00-00
                autoclose: true,//选择后是否自动关闭 
                minView: 0,//最精准的时间选择为日期  0-分 1-时 2-日 3-月
                language: 'zh-CN', //中文
                weekStart: 1, //一周从星期几开始
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                forceParse: 0,
                // daysOfWeekDisabled:"1,2,3", //一周的周几不能选 格式为"1,2,3"  数组格式也行
                todayBtn: true,  //在底部是否显示今天
                todayHighlight: false, //今天是否高亮显示
                keyboardNavigation: true, //方向图标改变日期  必须要有img文件夹 里面存放图标
                showMeridian: false,  //是否出现 上下午
                initialDate: new Date()
                //startDate: "2017-01-01" //日期开始时间 也可以是new Date()只能选择以后的时间
            }).on("changeDate", function () {
                var start = $("#dtStart").val();
                $("#end").datetimepicker("setStartDate", start);
            });
            $("#dtEnd").datetimepicker({
                format: 'yyyy-mm-dd hh:ii',  //格式  如果只有yyyy-mm-dd那就是0000-00-00
                autoclose: true,//选择后是否自动关闭 
                minView: 0,//最精准的时间选择为日期  0-分 1-时 2-日 3-月
                language: 'zh-CN', //中文
                weekStart: 1, //一周从星期几开始
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                forceParse: 0,
                //daysOfWeekDisabled:"1,2,3", //一周的周几不能选
                todayBtn: true,  //在底部是否显示今天
                todayHighlight: false, //今天是否高亮显示
                keyboardNavigation: true, //方向图标改变日期  必须要有img文件夹 里面存放图标
                showMeridian: false  //是否出现 上下午
                // startDate: "2017-01-01"  //开始时间  ENdDate 结束时间
            }).on("changeDate", function () {
                var end = $("#dtEnd").val();
                $("#dtStart").datetimepicker("setEndDate", end);
            });
        }

        function viewLog(obj) {
            $("#div_content").html($(obj).attr("val"));
            $('#myModal').modal('show');
        }

    </script>
</asp:Content>
