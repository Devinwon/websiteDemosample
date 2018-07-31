$(function () {
    //加载访客数据
    $.ajax({
        type: "post",
        url: "/Home/GetUserVisitLog",
        dataType: "json",
        data: "",
        success: function (data) {
            //在后台json转换Date(12839730837)为字符串
            //var obj = new Date(parseInt(data.PostDate.replace("/Date(", "").replace(")/", ""), 10));
            /* Line dashboard chart */
            var keyName = new Array();
            var linecolor = new Array();
            var i = 0;
            if (data != "") {
                for (var key in data.Table[0]) {
                    if (key != "PostDate") {
                        keyName[i] = key;
                        linecolor[i] = randomColor();
                        i++;
                    }
                }
                Morris.Line({
                    element: 'index-line-1',
                    data: data.Table,
                    xkey: 'PostDate',
                    ykeys: keyName,
                    labels: keyName,
                    resize: true,
                    hideHover: true,
                    gridTextSize: '10px',
                    lineColors: linecolor,
                    gridLineColor: '#E5E5E5'
                });
                /* EMD Line dashboard chart */
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
});
//自动生成颜色
function randomColor() {

    var colorStr = Math.floor(Math.random() * 0xFFFFFF).toString(16).toUpperCase();

    return "#" + "000000".substring(0, 6 - colorStr) + colorStr;

}