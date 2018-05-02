<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DeviceList.aspx.cs" Inherits="MSTSC.Manage.Web.DeviceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb nomargin">
        <li class="active">仪器列表
        </li>
    </ul>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 padding-right-10">
        <div class="panel panel-info search-panel margin-bottom-10 padding-bottom-10">
            <div class="quick-search-condition padding-5">
                <div class="form-inline text-center">
                    <label>产品类型</label>
                    <select id="quickptype" class="form-control margin-top-5 margin-bottom-5">
                        <option value="0"></option>
                        <option value="1">血球分析仪</option>
                        <option value="2">其他</option>
                    </select>
                    <input type="text" id="querytext" class="form-control margin-top-5 margin-bottom-5" value="" style="width: 280px;" placeholder="请输入仪器名称、SIM卡号或仪器序列号" />
                    <button type="button" id="quickquery" class="btn btn-default margin-top-5 margin-bottom-5 margin-left-10">快速定位</button>
                </div>
            </div>
            <hr class="nomargin" style="border-color: #cccccc; margin-left: 0px; margin-right: 0px;" />
            <div class="search-condition padding-5">
                <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12 col-lg-offset-2 col-md-offset-2 nopadding form-inline">
                    <div class="radio margin-left-20">
                        <label>
                            <input type="radio" name="rdoconnect" id="optionsRadios1" value="0" checked="checked" /> 所有仪器
                        </label>
                    </div>
                    <div class="radio margin-left-20">
                        <label>
                            <input type="radio" name="rdoconnect" id="optionsRadios2" value="1" /> 已连接仪器
                        </label>
                    </div>
                    <div class="radio margin-left-20">
                        <label>
                            <input type="radio" name="rdoconnect" id="optionsRadios3" value="2" /> 未连接仪器
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
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 nopadding">
        <div class="well well-sm padding-5 margin-right-5">
            <div class="panel panel-success nomargin">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">基本信息</h3>
                </div>
                <div class="panel-body nopadding">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>SIM卡号:</span><span class="margin-left-5" id="SIM"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>仪器序列号:</span><span class="margin-left-5" id="SN"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>产品类型:</span><span class="margin-left-5" id="ProductSeries"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>产品型号:</span><span class="margin-left-5" id="ProductModel"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>OEM代号:</span><span class="margin-left-5" id="OEM"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>代理商代号:</span><span class="margin-left-5" id="Agent"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>封闭试剂类型:</span><span class="margin-left-5" id="ReagentType"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>装机区域:</span><span class="margin-left-5" id="InstallationArea"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>出厂日期:</span><span class="margin-left-5" id="FactoryDate"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>装机日期:</span><span class="margin-left-5" id="InstallDate"></span>
                    </div>
                </div>
            </div>
            <div class="panel panel-success nomargin" style="margin-top: 15px;">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">仪器运行信息</h3>
                </div>
                <div class="panel-body nopadding">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>开机天数:</span><span class="margin-left-5" id="runtime_days"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>激光管运行时间:</span><span class="margin-left-5" id="runtime_opt"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>仪器运行时间:</span><span class="margin-left-5" id="runtime_power"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>气源运行时间:</span><span class="margin-left-5" id="runtime_air_supply"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>采样针穿刺次数:</span><span class="margin-left-5" id="needle_times_impale"></span>
                    </div>
                </div>
            </div>
            <div class="panel panel-success nomargin" style="margin-top: 15px;">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">计数信息统计</h3>
                </div>
                <div class="panel-body nopadding">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-5">
                        <span>计数总次数:</span><span class="margin-left-5" id="count_times_total"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>全血-CBC:</span><span class="margin-left-5" id="count_times_wb_cbc"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>全血-CBC+CRP:</span><span class="margin-left-5" id="count_times_wb_cbc_crp"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>全血-CRP:</span><span class="margin-left-5" id="count_times_wb_crp"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预稀释-CBC:</span><span class="margin-left-5" id="count_times_pd_cbc"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预稀释-CBC+CRP:</span><span class="margin-left-5" id="count_times_pd_cbc_crp"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预稀释-CRP:</span><span class="margin-left-5" id="count_times_pd_crp"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>全血-CBC+5DIFF:</span><span class="margin-left-5" id="count_times_wb_cd"></span>
                    </div>
                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-6 padding-5">
                        <span>预稀释-CBC+5DIFF:</span><span class="margin-left-5" id="count_times_pd_cd"></span>
                    </div>
                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-6 padding-5">
                        <span>全血-CBC+5DIFF+CRP:</span><span class="margin-left-5" id="count_times_wb_cd_crp"></span>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5">
                        <span>预稀释-CBC+5DIFF+CRP:</span><span class="margin-left-5" id="count_times_pd_cd_crp"></span>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-5">
                        <span>质控样本数:</span><span class="margin-left-5" id="count_times_qc"></span>
                    </div>
                </div>
            </div>
            <div class="panel panel-success nomargin" style="margin-top: 15px;">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">试剂耗量统计</h3>
                </div>
                <div class="panel-body nopadding">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>稀释液:</span><span class="margin-left-5" id="reagent_dil"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>LH溶血剂:</span><span class="margin-left-5" id="reagent_lh"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>CRP-R2:</span><span class="margin-left-5" id="reagent_r2"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>DIFF1:</span><span class="margin-left-5" id="reagent_diff1"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>DIFF2:</span><span class="margin-left-5" id="reagent_diff2"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>CRP-R1:</span><span class="margin-left-5" id="reagent_r1"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留1:</span><span class="margin-left-5" id="reagent_fl1"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留2:</span><span class="margin-left-5" id="reagent_fl2"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留3:</span><span class="margin-left-5" id="reagent_fl3"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留4:</span><span class="margin-left-5" id="reagent_fl4"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留5:</span><span class="margin-left-5" id="reagent_fl5"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留6:</span><span class="margin-left-5" id="reagent_fl6"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留7:</span><span class="margin-left-5"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留8:</span><span class="margin-left-5"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留9:</span><span class="margin-left-5"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>预留10:</span><span class="margin-left-5"></span>
                    </div>
                </div>
            </div>
            <div class="panel panel-success nomargin" style="margin-top: 15px;">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">故障统计信息</h3>
                </div>
                <div class="panel-body nopadding">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>WBC堵孔次数:</span><span class="margin-left-5" id="hole_times_wbc"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>RBC堵孔次数:</span><span class="margin-left-5" id="hole_times_rbc"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>采样组件故障次数:</span><span class="margin-left-5" id="sampling_times_fault"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>注射器故障次数:</span><span class="margin-left-5" id="syringe_times_syringe_fault"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>自动进样组件故障:</span><span class="margin-left-5" id="inject_times_fault"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>混匀组件故障次数:</span><span class="margin-left-5" id="mixing_times_fault"></span>
                    </div>
                </div>
            </div>
            <div class="padding-5"></div>
        </div>
    </div>

    <script type="text/javascript">
        var $table;
        var type = 0;
        var rows = 10;
        $(document).ready(function () {
            InitMainTable();

            $('#quickquery').click(function () {
                type = 1;
                if ($("#querytext").val().trim() == "") {
                    alert("请输入仪器名称、SIM卡号或仪器序列号！");
                    return;
                }
                $table.bootstrapTable('refresh', { url: 'DeviceList.aspx/getDeviceList' });
            });

            $('#query').click(function () {
                type = 2;
                $table.bootstrapTable('refresh', { url: 'DeviceQuery.aspx/getDeviceList' });
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
                        + "\",\"DeviceType\":\"" + $("#quickptype").find("option:selected").text()
                        + "\",\"QueryText\":\"" + $("#querytext").val()
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
                    $("#grid tbody tr").css("background-color", "");
                    $element.css("background-color", "#C0C0C0");

                    $.ajax({
                        type: "post",
                        url: "DeviceList.aspx/BindDetial",
                        data: "{'sn':'" + row.SN + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var result = eval(data.d)[0];
                            for (attribute in result) {
                                $("#" + attribute).html(result[attribute]);
                            }
                        },
                        error:function(err){
                            alert('error');
                        }
                    });
                }
            });
        };
    </script>
</asp:Content>
