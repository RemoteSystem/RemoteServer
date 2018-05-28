$(function () {
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
            $(this).parent().find(".sub-menu").slideToggle();
        })
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
            if (_strcurrenturl.indexOf(href) > -1 || isActive) {
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

//字符串转16进制
function strToHexCharCode(str) {
    if (str === "")
        return "";
    var hexCharCode = [];
    hexCharCode.push("0X");
    for (var i = 0; i < str.length; i++) {
        hexCharCode.push((str.charCodeAt(i)).toString(16));
    }
    return hexCharCode.join("");
}

//16进制转字符串
function hexCharCodeToStr(hexCharCodeStr) {
    var trimedStr = hexCharCodeStr.trim();
    var rawStr =
    trimedStr.substr(0, 2).toLowerCase() === "0x" ? trimedStr.substr(2) : trimedStr;
    var len = rawStr.length;
    if (len % 2 !== 0) {
        console.error("Illegal Format ASCII Code!");
        return "";
    }
    var curCharCode;
    var resultStr = [];
    for (var i = 0; i < len; i = i + 2) {
        curCharCode = parseInt(rawStr.substr(i, 2), 16); // ASCII Code Value
        resultStr.push(String.fromCharCode(curCharCode));
    }
    return resultStr.join("");
}

//16进制补0
function str_pad(hex) {
    var zero = '00000000';
    var tmp = 8 - hex.length;
    return zero.substr(0, tmp) + hex;
}