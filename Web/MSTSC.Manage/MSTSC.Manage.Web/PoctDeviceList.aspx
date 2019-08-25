<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PoctDeviceList.aspx.cs" Inherits="MSTSC.Manage.Web.PoctDeviceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">仪器查询</li>
    </ul>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 padding-right-10">
        <div class="panel panel-info search-panel margin-bottom-10 padding-bottom-5">
            <div class="panel-body nopadding">
                <div class="quick-search-condition padding-5">
                    <div class="form-inline text-center">
                        <select id="selType" class="form-control margin-top-5 margin-bottom-5">
                            <option value="POCT">POCT</option>
                        </select>
                        <input type="text" id="querytext" class="form-control margin-top-5 margin-bottom-5" value="" style="width: 275px;" placeholder="请输入仪器名称、SIM卡号或仪器序列号" />
                        <button type="button" id="quickquery" class="btn btn-default btn-normal margin-top-5 margin-bottom-5 margin-left-10">快速定位</button>
                    </div>
                </div>
                <hr class="nomargin" style="border-color: #cccccc; margin-left: 0px; margin-right: 0px;" />
                <div class="search-condition padding-5">
                    <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12 col-lg-offset-2 col-md-offset-2 nopadding form-inline">
                        <div class="radio margin-left-20">
                            <label>
                                <input type="radio" name="rdoconnect" id="optionsRadios1" value="0" checked="checked" />
                                所有仪器
                            </label>
                        </div>
                        <div class="radio margin-left-20">
                            <label>
                                <input type="radio" name="rdoconnect" id="optionsRadios2" value="1" />
                                已连接仪器
                            </label>
                        </div>
                        <div class="radio margin-left-20">
                            <label>
                                <input type="radio" name="rdoconnect" id="optionsRadios3" value="2" />
                                未连接仪器
                            </label>
                        </div>
                        <button type="button" id="query" class="btn btn-default btn-normal margin-top-5 margin-bottom-5 margin-left-20">查 询</button>
                    </div>
                </div>
                <span class="clearfix"></span>
            </div>
        </div>

        <div class="panel">
            <table id="grid"></table>
        </div>
    </div>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 nopadding">
        <div class="well well-sm padding-5 margin-right-5">
            <div class="padding-5 form-inline">
                <span>数据更新时间:&nbsp;&nbsp;</span><span id="UpdateTime"></span>
                <div class="pull-right">
                    <button type="button" id="refresh" class="btn btn-default btn-small" style="padding: 1px 5px;">刷 新</button>
                </div>
                <span class="clearfix"></span>
            </div>
            <div class="panel panel-success nomargin">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">基本信息</h3>
                </div>
                <div class="panel-body nopadding">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>仪器名称:</span><span class="margin-left-5" id="devicename"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>SIM卡号:</span><span class="margin-left-5" id="SIM"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>仪器序列号:</span><span class="margin-left-5" id="SN"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>装机区域:</span><span class="margin-left-5" id="Region"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>医院地址:</span><span class="margin-left-5" id="Address"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>医院名称:</span><span class="margin-left-5" id="Hospital"></span>
                    </div>
                </div>
            </div>
            <div class="panel panel-success nomargin" style="margin-top: 15px;">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">统计信息</h3>
                </div>
                <div class="panel-body nopadding">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-5">
                        <span>仪器已测样本总量:</span><span class="margin-left-5" id="sample"></span>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-5">
                        <table id="sample_grid"></table>
                    </div>
                </div>
            </div>
            <div class="panel panel-success nomargin" style="margin-top: 15px;">
                <div class="panel-heading padding-5">
                    <span class="panel-title">故障信息</span>
                </div>
                <div class="padding-top-10 padding-bottom-10" style="background-color: #d9eaf9;">
                    <div class="pull-right" style="min-width:450px;">
                        故障发生时间：  
                        <input type="text" id="dtstart" placeholder="开始时间" style="width: 100px;" />
                        -
                        <input type="text" id="dtend" placeholder="结束时间" style="width: 100px;" />
                        <button type="button" id="faultSearch" class="btn btn-default btn-small margin-left-10" style="padding: 1px 5px;">查询</button>
                        <button type="button" id="faultExport" class="btn btn-default btn-small margin-left-5" style="padding: 1px 5px;">导 出</button>
                    </div>
                    <span class="clearfix"></span>
                </div>
                <div id="fault" class="panel-body nopadding">
                     <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 padding-5 text-center">
                        <span>序号</span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 padding-5 text-center">
                        <span>错误码</span>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5 text-center">
                        <span>时间</span>
                    </div>
                </div>
            </div>
            <div class="padding-5"></div>
        </div>
    </div>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width:700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×
                    </button>
                    <h5 class="modal-title">远程诊断系统--项目详情</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-4 col-md-5 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">项目编号：</label>
                            <label id="num" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5 form-inline">
                            <label style="width: 100px; text-align: right">测试卡批次：</label>
                            <select id="selLot" style="width: 90px;" class="form-control margin-left-5"></select>
                            <button type="button" id="btnLot" class="btn btn-default">查询</button>
                        </div>
                    </div>
                    <hr style="margin: 5px auto;" />
                    <div class="row">
                        <div class="col-lg-4 col-md-5 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">测试项目：</label>
                            <label id="card_name" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">孵育时间：</label>
                            <label id="incubate_time" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-4 col-md-5 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">项目1名称：</label>
                            <label id="analyte_1" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">项目1参数：</label>
                            <label id="analyte_1_params" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-4 col-md-5 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">项目2名称：</label>
                            <label id="analyte_2" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">项目2参数：</label>
                            <label id="analyte_2_params" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-4 col-md-5 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">项目3名称：</label>
                            <label id="analyte_3" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">项目3参数：</label>
                            <label id="analyte_3_params" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-4 col-md-5 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">T线数：</label>
                            <label id="signals" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5">
                            <label style="width: 100px; text-align: right">有效期：</label>
                            <label id="expiry" style="margin-left: 5px; width: 80px; font-weight: normal;"></label>
                        </div>
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
        var sn = "";
        var $sample_grid;

        var tabletimer;
        var rowtimer;
        $(document).ready(function () {
            InitMainTable();
            InitDateTimePicker();

            $('#dtstart').datetimepicker({ format: 'YYYY-MM-DD' });
            $('#dtend').datetimepicker({ format: 'YYYY-MM-DD' });
            var now = new Date();
            $('#dtstart').val(now.Format("yyyy-MM-dd"));
            $('#dtend').val(now.Format("yyyy-MM-dd"));

            $('#quickquery').click(function () {
                type = 1;

                sn = "";
                clearTimeout(tabletimer);
                $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'PoctDeviceQuery.aspx/getDeviceList' });
            });

            $('#query').click(function () {
                type = 2;
                var page = 1;
                var sort = "";
                var order = "";
                sn = "";

                clearTimeout(tabletimer);
                $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'PoctDeviceQuery.aspx/getDeviceList' });
            });

            $('#refresh').click(function () {
                getRowInfo();
            });

            $('#btnLot').click(function () {
                var lot = $("#selLot").val();
                var num = $("#myModal").attr("num");
                getNumDetail($("#myModal"), sn, num, lot);
            });

            $('#faultExport').click(function () {
                if (sn) {
                    var dtstart = $('#dtstart').val() + " 00:00";
                    var dtend = $('#dtend').val() + " 23:59";
                    var conditions = "{'sn':'" + sn + "','dtstart':'" + dtstart + "','dtend':'" + dtend + "'}";
                    window.open("Export.ashx?Action=poct_fault&conditions=" + conditions, "_blank");
                }
            });

            $("#faultSearch").click(function () {
                getFault();
            });

            //getModels();
            getRegion();
            $('#quickquery').click();
        });
        //初始化bootstrap-table的内容
        function InitMainTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'PoctDeviceQuery.aspx/getDeviceList';
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
                    var conditions = "{\"QueryRange\":\"" + (type == 1 ? "" : $('input[name="rdoconnect"]:checked').val())
                         + "\",\"DeviceType\":\"POCT"
                        + "\",\"QueryText\":\"" + (type != 1 ? "" : $("#querytext").val())
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
                    if (!sn && json && json.rows.length) {
                        sn = json.rows[0].SN;
                        getRowInfo();
                    }
                    return json;
                },
                columns: [
                    {
                        field: 'DeviceName',
                        title: '仪器名称',
                        align: "center"
                    }, {
                        field: 'SIM',
                        title: 'SIM卡号',
                        align: "center"
                    }, {
                        field: 'SN',
                        title: '仪器序列号',
                        align: "center"
                    }, {
                        field: 'Model',
                        title: '仪器型号',
                        align: "center"
                    }, {
                        field: 'Region',
                        title: '装机区域',
                        align: "center"
                    }, {
                        field: 'SESSION_ID',
                        title: '状态',
                        align: "center",
                        formatter: function (value, row, index) {
                            var div = "<span>" + value + "</span>";
                            return div;
                        }
                    }],
                onLoadSuccess: function () {
                    if (type != 0) {
                        tabletimer = setTimeout(freshTable, 120000);

                        var r = $("[data-uniqueid='" + sn + "']");
                        r.css("background-color", "#C0C0C0");
                        if (r.length == 0) {
                            sn = "";
                            clearInfo();
                        }
                    }
                    InitSampleTable();
                },
                onLoadError: function () {
                    clearTimeout(tabletimer);
                    //alert("数据加载失败！");
                },
                onSort: function (name, order) {
                    clearTimeout(tabletimer);
                },
                onPageChange: function (name, order) {
                    clearTimeout(tabletimer);
                },
                onDblClickRow: function (row, $element) {
                    $("#grid tbody tr").css("background-color", "");
                    $element.css("background-color", "#C0C0C0");

                    sn = row.SN;
                    var now = new Date();
                    $('#dtstart').val(now.Format("yyyy-MM-dd"));
                    $('#dtend').val(now.Format("yyyy-MM-dd"));
                    getRowInfo();
                }
            });
        }

        function freshTable() {
            clearTimeout(tabletimer);
            $table.bootstrapTable('refreshOptions', { url: 'PoctDeviceQuery.aspx/getDeviceList' });
        }

        function getRowInfo() {
            clearTimeout(rowtimer);
            if (sn) {
                $.ajax({
                    type: "post",
                    url: "PoctDeviceQuery.aspx/getPoctDeviceDetial",
                    data: "{'sn':'" + sn + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var result = eval(data.d)[0];
                        for (attribute in result) {
                            $("#" + attribute).html(result[attribute]);
                        }
                        getFault();
                        freshSampleTable();
                        rowtimer = setTimeout(getRowInfo, 61000);
                    },
                    error: function (err) {
                        alert('获取数据出错.');
                    }
                });
            } else {
                clearInfo();
            }
        }

        //初始化bootstrap-table的内容
        function InitSampleTable() {
            //记录页面bootstrap-table全局变量$table，方便应用
            var queryUrl = 'PoctDeviceQuery.aspx/getDeviceSampleList';
            $sample_grid = $('#sample_grid').bootstrapTable({
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
                    var temp = "{"
                    + "'conditions':'" + sn
                    + "'}";
                    return temp;
                },
                responseHandler: function (res) {
                    return JSON.parse(res.d)["rows"];
                },
                columns: [
                    {
                        title: '序号',
                        align: "center",
                        width: 40,
                        formatter: function (value, row, index) {
                            return index + 1;
                        }
                    },
                    {
                        field: 'num',
                        title: '项目编号',
                        align: "center"
                    }, {
                        field: 'smpl',
                        title: '样本数',
                        align: "center"
                    }, {
                        field: 'card_consume',
                        title: '测试卡消耗数',
                        align: "center"
                    }, {
                        field: 'num',
                        title: '项目详情',
                        width: 80,
                        align: "center",
                        formatter: function (value, row, index) {
                            return '<a href="javascript:void(0)" onclick="showDetail(\'' + value + '\');return false;">查看</a>';
                        }
                    }]
            });
        }

        function freshSampleTable() {
            $sample_grid.bootstrapTable('refreshOptions', { url: 'PoctDeviceQuery.aspx/getDeviceSampleList' });
        }

        function getModels() {
            $.ajax({
                type: "post",
                url: "PoctDeviceQuery.aspx/getModel",
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
                url: "PoctDeviceQuery.aspx/getRegion",
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

        function getFault() {
            if (!sn) {
                return;
            }
            var dtstart = $('#dtstart').val() + " 00:00";
            var dtend = $('#dtend').val() + " 23:59";
            $.ajax({
                type: "post",
                url: "PoctDeviceQuery.aspx/getPoctDeviceFault",
                data: "{'sn':'" + sn + "','dtstart':'" + dtstart + "','dtend':'" + dtend + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval(data.d);
                    var str = '';

                    var i = 1;
                    for (res in result) {
                        str += '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 padding-bottom-5 text-center">' + i + '</div><div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 padding-bottom-5 text-center"><span>' + result[res]['code'] + '</span></div><div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-bottom-5 text-center"><span>' + result[res]['dttime'] + '</span></div>';
                        i += 1;
                    }
                    if (!str) {
                        str = '<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-5 text-center"><span>无错误信息</span></div>';
                    } else {
                        str = '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 padding-bottom-5 text-center">序号</div><div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 padding-top-5 text-center"><span>错误码</span></div><div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-top-5 text-center"><span>时间</span></div>' + str;
                        str = str.replace(/T/g, " ").replace(/Z/g, "");
                    }

                    $("#fault").html(str);
                },
                error: function (err) {
                }
            });
        }

        function showDetail(num) {
            console.info(num);
            $("#myModal").attr("num", num);
            $.ajax({
                type: "post",
                url: "PoctDeviceQuery.aspx/getNumLotList",
                data: "{'sn':'" + sn + "','num':'" + num + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval(data.d);
                    $("#selLot").html("");
                    var opts = '';
                    for (res in result) {
                        opts += "<option value=\"" + result[res]['key'] + "\">" + result[res]['value'] + "</option>";
                    }
                    $("#selLot").html(opts);
                    $('#myModal').modal('show');
                },
                error: function (err) {
                }
            });
        }

        $('#myModal').on('show.bs.modal', function (event) {
            var modal = $(this);
            var num = $("#myModal").attr("num");
            getNumDetail(modal, sn, num, '');
        });

        function getNumDetail(modal, sn, num, lot) {
            $.ajax({
                type: "post",
                url: "PoctDeviceQuery.aspx/getNumDetail",
                data: "{'sn':'" + sn + "','num':'" + num + "','lot':'" + lot + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval("[" + data.d + "]")[0];
                    for (attribute in result) {
                        if (modal.find("#" + attribute)) modal.find("#" + attribute).html(result[attribute]);                       
                    }

                    if (modal.find("#incubate_time").html()) modal.find("#incubate_time").html(modal.find("#incubate_time").html() + " s");
                },
                error: function (err) {
                }
            });
        }

        function clearInfo() {
            $(".margin-left-5").html("");
            $("#UpdateTime").html("");
            freshSampleTable();
            $("#fault").html('<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-5 text-center"><span>无错误信息</span></div>');
        }

    </script>
</asp:Content>
