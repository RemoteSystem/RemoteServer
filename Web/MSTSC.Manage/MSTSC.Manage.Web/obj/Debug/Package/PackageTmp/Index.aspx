<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MSTSC.Manage.Web.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="breadcrumb">
        <li class="active">首页</li>
    </ul>
    <div class="col-sm-12">
        <div class="jumbotron">
            <h1>这是一套开源的基于bootstrap的简易框架</h1>
            <p>所用插件和库几乎都属于开源（个别特殊的会做说明），且功能正在不间断更新中</p>
        </div>
        <ul class="list-group">
            <li class="list-group-item active">核心库（现在页面呈现效果必须依赖的库）
                    </li>
            <li class="list-group-item">jquery-1.12.4.js（<a href="https://jquery.com/" target="_blank">官网</a>）
                    </li>
            <li class="list-group-item">bootstrap3.3.6（<a href="https://getbootstrap.com/" target="_blank">官网</a>）
                    </li>
            <li class="list-group-item">font-awesome4.6.3（<a href="http://fontawesome.io/" target="_blank">官网</a>）
                    </li>
            <li class="list-group-item">
                <p>让页面返回顶部时平滑滚动（<a href="sample/scrolltop.html" target="_blank">样例</a>）</p>
                <p>使用说明：页面需要引用scrolltopcontrol.js</p>
            </li>
            <li class="list-group-item">
                <span class="badge">2016.4.28</span>
                <p>让bootstrap的carousel插件支持手势（<a href="sample/carousel.html" target="_blank">样例</a>）（<a href="https://github.com/hammerjs/jquery.hammer.js" target="_blank">jquery.hammer官网</a>）（<a href="http://hammerjs.github.io/" target="_blank">hammer.js官网</a>）</p>
                <p>
                    使用说明：页面需要引用hammer.min.js、jquery.hammer.js
                       
                </p>
                <p>注意：已将carousel的手势绑定事件封装到了default.js中，如果页面已经引用了default.js，就不用再给carousel写手势绑定事件了</p>
            </li>
            <li class="list-group-item">
                <span class="badge">2016.7.1</span>
                <p>页面加载进度条（<a href="sample/pace.html" target="_blank">样例</a>）（<a href="http://github.hubspot.com/pace/docs/welcome/" target="_blank">官网</a>）</p>
                <p>
                    使用说明：1.页面需要引用pace.min.js、dataurl.css
                            <br />
                    2.官网提供多种样式供选择，只需要下载相应的css文件替换掉原来的css文件就行<br />
                    3.dataurl.css里面可以调整进度条位置
                       
                </p>
            </li>
        </ul>
        <ul class="list-group">
            <li class="list-group-item active">更新日志
                    </li>
            <li class="list-group-item">
                <span class="badge">Form库&nbsp;2016.4.19</span>
                <a href="form.html?#form-DateRangePicker">加入bootstrap-DateRangePicker时间范围选择插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Message库&nbsp;2016.4.20</span>
                <a href="message.html?#message-toastr">加入toastr通知插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Message库&nbsp;2016.4.20</span>
                <a href="message.html?#message-sweetalert">加入bootstrap-sweetalert通知插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">UI库&nbsp;2016.4.20</span>
                <a href="ui.html?#ui-metisMenu">加入metisMenu菜单样式特效库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">UI库&nbsp;2016.4.20</span>
                <a href="ui.html?#ui-vide">加入vide.js把视频作为背景特效库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Form库&nbsp;2016.4.21</span>
                <a href="form.html?#form-colorpicker">加入bootstrap-colorpicker颜色选择插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Animate库&nbsp;2016.4.21</span>
                <a href="animate.html?#animate-wow">加入WOW特效库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Animate库&nbsp;2016.4.21</span>
                <a href="animate.html?#animate-hover">加入Hover.css特效库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Animate库&nbsp;2016.4.21</span>
                <a href="animate.html?#animate-animo">加入animo特效库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">UI库&nbsp;2016.4.22</span>
                <a href="ui.html?#ui-switch">加入bootstrap-switch开关插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Form库&nbsp;2016.4.22</span>
                <a href="form.html?#form-datepicker">加入bootstrap-datepicker日期选择插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Form库&nbsp;2016.4.28</span>
                <a href="form.html?#form-datetimepicker">加入bootstrap-datetimepicker日期时间选择插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Form库&nbsp;2016.4.28</span>
                <a href="form.html?#form-formhelper">加入bootstrap-formhelper插件集合库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">UI库&nbsp;2016.4.29</span>
                <a href="ui.html?#ui-masonry">加入masonry流式布局插件&nbsp;<label class="label label-danger">强烈推荐</label></a>
            </li>
            <li class="list-group-item">
                <span class="badge">UI库&nbsp;2016.5.4</span>
                <a href="ui.html?#ui-tabdrop">加入bootstrap-tab超出自动折叠插件</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Carousel库&nbsp;2016.9.10</span>
                <a href="carousel.html?#carousel-fotorama">加入jquery-fotorama图片滚动库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Carousel库&nbsp;2016.9.10</span>
                <a href="carousel.html?#carousel-owlcarousel2">加入jquery-owlcarousel2图片滚动库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Carousel库&nbsp;2016.9.10</span>
                <a href="carousel.html?#carousel-owlcarousel">加入jquery-owlcarousel图片滚动库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Carousel库&nbsp;2016.9.10</span>
                <a href="carousel.html?#carousel-fullpage">加入jquery-fullpage图片滚动库</a>
            </li>
            <li class="list-group-item">
                <span class="badge">UI库&nbsp;2016.9.10</span>
                <a href="ui.html?#ui-mmenu">加入jquery-mmenu Demo</a>
            </li>
            <li class="list-group-item">
                <span class="badge">Chart库&nbsp;2016.9.10</span>
                <a href="chart.html?chart-flot">加入jquery-flot库</a>
            </li>
        </ul>
        <ul class="list-group">
            <li class="list-group-item active">代码示例
                    </li>
            <li class="list-group-item">
                <span class="badge">2016.6.24</span>
                <a href="sample/iframe/parent.html" target="_blank">嵌套响应式iframe</a>
            </li>
        </ul>
    </div>
</asp:Content>
