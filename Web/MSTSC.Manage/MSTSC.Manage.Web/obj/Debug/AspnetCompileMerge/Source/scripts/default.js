﻿$(function () {
    pageInitModule.setWidth();
    pageInitModule.setSidebar();
    pageInitModule.setCarousel();
})
$(window).resize(function () {
    pageInitModule.setWidth();
})
$(window).scroll(function () {
    pageInitModule.setScrollToTop();
});

/*
* init page when page load
*/
var pageInitModule = (function (mod) {
    mod.setCarousel = function () {
        try {
            $('.carousel').hammer().on('swipeleft', function () {
                $(this).carousel('next');
            });
            $('.carousel').hammer().on('swiperight', function () {
                $(this).carousel('prev');
            });
        } catch (e) {
            console.log("you mush import hammer.js and jquery.hammer.js to let the carousel can be touched on mobile");
        }
    };
    mod.setWidth = function () {
        if ($(window).width() < 768) {
            $(".sidebar").css({ left: -180 });
            $(".all").css({ marginLeft: 0 });
        } else {
            $(".sidebar").css({ left: 0 });
            $(".all").css({ marginLeft: 180 });
        }
    };
    mod.setScrollToTop = function () {
        var top = $(window).scrollTop();
        if (top < 60) {
            $('#goTop').hide();
        } else {
            $('#goTop').show();
        }
    };
    mod.setSidebar = function () {
        $('[data-target="sidebar"]').click(function () {
            var asideleft = $(".sidebar").offset().left;
            if (asideleft == 0) {
                $(".sidebar").animate({ left: -180 });
                $(".all").animate({ marginLeft: 0 });
            }
            else {
                $(".sidebar").animate({ left: 0 });
                $(".all").animate({ marginLeft: 180 });
            }
        });
        $(".has-sub>a").click(function () {
            $(this).parent().siblings().find(".sub-menu").slideUp();
            $(this).parent().parent().siblings().find(".sub-menu").slideUp();
            $(this).parent().find(".sub-menu").slideToggle();
        });

        $(".nav-header").click(function () {
            $(this).parent().find(".nav").slideToggle();
        });

        var _strcurrenturl = window.location.href.toLowerCase();
        $(".navbar-nav a[href],.sidebar a[href]").each(function () {
            var href = $(this).attr("href").toLowerCase();
            var isActive = false;
            $(".breadcrumb>li a[href]").each(function (index) {
                if (href == $(this).attr("href").toLowerCase()) {
                    isActive = true;
                    return false;
                }
            })
            if (_strcurrenturl.indexOf("/" + href) > -1 || isActive) {
                $(this).parent().addClass("active");
                if ($(this).parents("ul").attr("class") == "sub-menu") {
                    $(this).parents("ul").show();//原来是slideDown()，有滑动效果
                    $(this).parents(".has-sub").addClass("active");
                }
            }
        })
    }
    return mod;
})(window.pageInitModule || {});

//Date的prototype 属性可以向对象添加属性和方法。   
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "H+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "S+": this.getMilliseconds()
    };
    //因为date.getFullYear()出来的结果是number类型的,所以为了让结果变成字符串型，下面有两种方法：
    if (/(y+)/.test(fmt)) {
        //第一种：利用字符串连接符“+”给date.getFullYear()+""，加一个空字符串便可以将number类型转换成字符串。
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            //第二种：使用String()类型进行强制数据类型转换String(date.getFullYear())，这种更容易理解。
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(String(o[k]).length)));
        }
    }
    return fmt;
};

function InitDateTimePicker() {
    //if (!$("#dtstart") || !$("#dtend")) {
    //    return;
    //}

    //$("#dtstart").datetimepicker({
    //    format: 'yyyy-mm-dd hh:ii',  //格式  如果只有yyyy-mm-dd那就是0000-00-00
    //    autoclose: true,//选择后是否自动关闭 
    //    minView: 0,//最精准的时间选择为日期  0-分 1-时 2-日 3-月
    //    language: 'zh-CN', //中文
    //    weekStart: 1, //一周从星期几开始
    //    pickerPosition: "top-left",
    //    startView: 2,
    //    forceParse: 0,
    //    // daysOfWeekDisabled:"1,2,3", //一周的周几不能选 格式为"1,2,3"  数组格式也行
    //    todayBtn: true,  //在底部是否显示今天
    //    todayHighlight: true, //今天是否高亮显示
    //    keyboardNavigation: true, //方向图标改变日期  必须要有img文件夹 里面存放图标
    //    showMeridian: false,  //是否出现 上下午
    //    initialDate: new Date()
    //    //startDate: "2017-01-01" //日期开始时间 也可以是new Date()只能选择以后的时间
    //}).on("changeDate", function () {
    //    var start = $("#dtstart").val();
    //    $("#dtend").datetimepicker("setStartDate", start);
    //});
    //$("#dtend").datetimepicker({
    //    format: 'yyyy-mm-dd hh:ii',  //格式  如果只有yyyy-mm-dd那就是0000-00-00
    //    autoclose: true,//选择后是否自动关闭 
    //    minView: 0,//最精准的时间选择为日期  0-分 1-时 2-日 3-月
    //    language: 'zh-CN', //中文
    //    weekStart: 1, //一周从星期几开始
    //    pickerPosition: "top-left",
    //    startView: 2,
    //    forceParse: 0,
    //    //daysOfWeekDisabled:"1,2,3", //一周的周几不能选
    //    todayBtn: true,  //在底部是否显示今天
    //    todayHighlight: true, //今天是否高亮显示
    //    keyboardNavigation: true, //方向图标改变日期  必须要有img文件夹 里面存放图标
    //    showMeridian: false  //是否出现 上下午
    //    // startDate: "2017-01-01"  //开始时间  ENdDate 结束时间
    //}).on("changeDate", function () {
    //    var end = $("#dtend").val();
    //    $("#dtstart").datetimepicker("setEndDate", end);
    //});
}