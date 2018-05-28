<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DeviceQuery.aspx.cs" Inherits="MSTSC.Manage.Web.DeviceQuery" %>

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
                            <option value="0">请选择产品类型</option>
                            <option value="血液细胞分析仪">血液细胞分析仪</option>
                        </select>
                        <input type="text" id="querytext" class="form-control margin-top-5 margin-bottom-5" value="" style="width: 275px;" placeholder="请输入仪器名称、SIM卡号或仪器序列号" />
                        <button type="button" id="quickquery" class="btn btn-default btn-normal margin-top-5 margin-bottom-5 margin-left-10">快速定位</button>
                    </div>
                </div>
                <hr class="nomargin" style="border-color: #cccccc; margin-left: 0px; margin-right: 0px;" />
                <div class="search-condition padding-5">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                        <label>产品系列</label>
                        <select id="selSeries" class="form-control margin-top-5 margin-bottom-5">
                            <option value="0">请选择</option>
                            <option value="3diff">三分类</option>
                            <option value="5diff">五分类</option>
                        </select>
                    </div>

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                        <label>产品型号</label>
                        <select id="selModel" class="form-control margin-top-5 margin-bottom-5">
                            <option value="0">请选择</option>
                        </select>
                    </div>

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 nopadding form-inline">
                        <label>OEM代号</label>
                        <select id="oemCdoe" class="form-control margin-top-5 margin-bottom-5">
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
            <%--<div class="padding-left-5" style="font-weight:bold;font-size:15pt;">
              远程诊断系统 - <span id="devicename">仪器名称</span>
            </div>--%>
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
                        <span>产品类型:</span><span class="margin-left-5" id="DeviceType"></span>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-5">
                        <span>产品型号:</span><span class="margin-left-5" id="Model"></span>
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
                        <span>装机区域:</span><span class="margin-left-5" id="Region"></span>
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
            <div class="panel panel-success nomargin" style="margin-top: 15px;">
                <div class="panel-heading padding-5">
                    <h3 class="panel-title">错误信息</h3>
                </div>
                <div id="fault" class="panel-body nopadding">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-5 text-center">
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

    <script type="text/javascript">
        var $table;
        var type = 0;
        var rows = 10;
        var page = 1;
        var sort = "";
        var order = "";
        var sn = "";

        var tabletimer;
        var rowtimer;
        $(document).ready(function () {
            InitMainTable();

            var opts = "<option value=\"0\">请选择</option>";
            for (var i = 1; i < 100; i++) {
                opts += "<option value=\"" + i + "\">" + i + "</option>";
            }
            $("#oemCdoe").html(opts);
            $("#agent").html(opts);

            $('#quickquery').click(function () {
                type = 1;
                //if ($("#querytext").val().trim() == "") {
                //    alert("请输入仪器名称、SIM卡号或仪器序列号！");
                //    return;
                //}
                clearTimeout(tabletimer);
                $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'DeviceQuery.aspx/getDeviceList' });
            });

            $('#query').click(function () {
                type = 2;
                var page = 1;
                var sort = "";
                var order = "";

                clearTimeout(tabletimer);
                $table.bootstrapTable('refreshOptions', { pageNumber: 1, url: 'DeviceQuery.aspx/getDeviceList' });
            });

            $('#refresh').click(function () {
                getRowInfo();
            });

            getTypes();
            getSeries();
            getModels();
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
                        + "\",\"DeviceType\":\"" + (type != 1 ? "" : $("#selType").val())
                        + "\",\"QueryText\":\"" + (type != 1 ? "" : $("#querytext").val())
                        + "\",\"ProductSeries\":\"" + (type == 1 ? "" : $("#selSeries").val())
                        + "\",\"ModelType\":\"" + (type == 1 ? "" : $("#selModel").val())
                        + "\",\"OEM\":\"" + (type == 1 ? "" : $("#oemCdoe").val())
                        + "\",\"Agent\":\"" + (type == 1 ? "" : $("#agent").val())
                        + "\",\"ReagentType\":\"" + (type == 1 ? "" : $("#reagenttype").val())
                        + "\",\"Region\":\"" + (type == 1 ? "" : $("#region").val())
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
                        field: 'ProductSeries',
                        title: '产品系列',
                        formatter: function (value, row, index) {
                            var val = (value == "3diff" ? "三分类" : (value == "5diff" ? "五分类" : ""));
                            return val;
                        }
                    }, {
                        field: 'Model',
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
                    if (type != 0) {
                        tabletimer = setTimeout(freshTable, 30000);

                        var r = $("[data-uniqueid='" + sn + "']");
                        r.css("background-color", "#C0C0C0");
                    }
                },
                onLoadError: function () {
                    clearTimeout(tabletimer);
                    alert("数据加载失败！");
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
                    getRowInfo();
                }
            });
        };

        function freshTable() {
            clearTimeout(tabletimer);
            $table.bootstrapTable('refreshOptions', { url: 'DeviceQuery.aspx/getDeviceList' });
        }

        function getRowInfo() {
            clearTimeout(rowtimer);
            if (sn) {
                $.ajax({
                    type: "post",
                    url: "DeviceQuery.aspx/BindDetial",
                    data: "{'sn':'" + sn + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var result = eval(data.d)[0];
                        for (attribute in result) {
                            $("#" + attribute).html(result[attribute]);
                        }
                        getFault();
                        rowtimer = setTimeout(getRowInfo, 13000);
                    },
                    error: function (err) {
                        alert('获取数据出错.');
                    }
                });
            } else {
                alert("请先选择需要查看的行！");
            }
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

        function getFault() {
            $.ajax({
                type: "post",
                url: "DeviceQuery.aspx/GetDeviceFault",
                data: "{'sn':'" + sn + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = eval(data.d);
                    var str = '';

                    for (res in result) {
                        str += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-bottom-5 text-center"><span>0x' + str_pad(parseInt(result[res]['code']).toString(16)) + '</span></div><div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-bottom-5 text-center"><span>' + result[res]['dttime'] + '</span></div>';
                    }
                    if (!str) {
                        str = '<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-5 text-center"><span>无错误信息</span></div>';
                    } else {
                        str = '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-top-5 text-center"><span>错误码</span></div><div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 padding-top-5 text-center"><span>时间</span></div>' + str;
                        str = str.replace(/T/g, " ").replace(/Z/g, "");
                    }

                    $("#fault").html(str);
                },
                error: function (err) {
                }
            });
        }

    </script>
</asp:Content>
