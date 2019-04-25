Highcharts.setOptions({
    lang: {
        contextButtonTitle: "图表导出菜单",
        decimalPoint: ".",
        downloadJPEG: "下载JPEG图片",
        downloadPDF: "下载PDF文件",
        downloadPNG: "下载PNG文件",
        downloadSVG: "下载SVG文件",
        drillUpText: "返回 {series.name}",
        loading: "加载中",
        months: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        noData: "没有数据",
        numericSymbols: ["千", "兆", "G", "T", "P", "E"],
        printChart: "打印图表",
        resetZoom: "恢复缩放",
        resetZoomTitle: "恢复图表",
        shortMonths: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
        thousandsSep: "",//不用千分位逗号分开的形式
        weekdays: ["星期一", "星期二", "星期三", "星期三", "星期四", "星期五", "星期六", "星期天"]
    },
    colors: ['#058dc7', '#ed561b', '#50b432', '#f7a35c', '#8085e9', '#f15c80', '#e4d354', '#8085e8', '#8d4653', '#91e8e1']
});

function initPie(id, title) {
    var chart = $('#' + id).highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            marginRight: 50
        },
        title: {
            text: title,
            style: {
                color: '#333',
                fontWeight: "bold",
                fontSize: '16px'
            }
        },
        tooltip: {
            headerFormat: '',
            pointFormat: '{point.name} <br/><b>{point.y}</b><br/><b>{point.percentage:.1f}%</b>'
        },
        legend: {
            //控制图例显示位置
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            itemMarginBottom: 10
        },
        credits: {
            enabled: false
        },
        lang: {
            noData: "无数据显示" //真正显示的文本
        },
        noData: {
            // Custom positioning/aligning options  
            position: {    //相对于绘图区定位无数据标签的位置。 默认值：[object Object].
                //align: 'right',  
                //verticalAlign: 'bottom'  
            },
            // Custom svg attributes  
            attr: {     //无数据标签中额外的SVG属性
                //'stroke-width': 1,  
                //stroke: '#cccccc'  
            },
            // Custom css  
            style: {    //对无数据标签的CSS样式。 默认值：[object Object].                    
                //fontWeight: 'bold',       
                fontSize: '15px',
                color: '#555555'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                showInLegend: true,
                dataLabels: {
                    enabled: true,
                    format: '{point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    },
                    connectorPadding: 0,
                    distance: -30
                },
                states: {
                    hover: {
                        enabled: false
                    }
                },
                slicedOffset: 20,         // 突出间距
                point: {                  // 每个扇区是数据点对象，所以事件应该写在 point 下面
                    events: {
                        // 鼠标滑过是，突出当前扇区
                        mouseOver: function () {
                            this.slice();
                        },
                        // 鼠标移出时，收回突出显示
                        mouseOut: function () {
                            this.slice();
                        },
                        // 默认是点击突出，这里屏蔽掉
                        click: function () {
                            return false;
                        }
                    }
                }
            }
        },
        series: [{
            type: 'pie',
            name: '',
            data: []
        }],
        exporting: {
            enabled: true,
            buttons: {
                contextButton: {
                    menuItems: [{
                        text: '打印图片',
                        onclick: function () {
                            this.print();
                        }
                    }, {
                        text: '导出PNG图片',
                        onclick: function () {
                            this.exportChart();
                        },
                        separator: false
                    }]
                }
            }
        }
    });

    return chart;
}