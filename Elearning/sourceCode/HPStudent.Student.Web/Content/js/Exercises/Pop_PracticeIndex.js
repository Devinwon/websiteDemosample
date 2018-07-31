//专业下拉框变化
$('#selMajor').change(function () {
    var mid = $(this).val();
    //ajax加载课程数据
    $.ajax({
        type: "post",
        url: "/Exercises/GetStatistics",
        dataType: "json",
        data: "mid=" + mid,
        success: function (data) {
            var JsonSource = eval(data);
            $("#indicatorContainerPanel").empty();
            if (JsonSource != null) {
                var htmlStr = SetDivValue(JsonSource);
                $("#indicatorContainerPanel").append(htmlStr);
                //加载圆圈数据值
                SetStatistics(JsonSource);
                onresize();
            }
            else {
                $("#indicatorContainerPanel").append(" 暂无数据，请选择专业");
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
});
//加载报表html数据
function SetDivValue(JsonSource) {
    var htmlStr = "";
    var i = 1;
    $.each(JsonSource, function (key, val) {
        htmlStr += "<div class=\"col-md-2 col-xs-12\">";
        htmlStr += " <a href=\"#\"  class=\"tile tile-info\">";
        htmlStr += "<div onclick=\"PraticeClick(" + JsonSource[key].CID + ")\" id=\"indicatorContainerWrap" + i + "\" >";
        htmlStr += "<div id=\"indicatorContainer" + i + "\"></div>";
        htmlStr += "<p>" + JsonSource[key].CategoryName + "</p> <p>" + JsonSource[key].TestNum + "/" + JsonSource[key].QuestionNums + "</p>";
        htmlStr += "</div>";
        htmlStr += "</a>"
        htmlStr += "</div>";
        i++;
    });
    return htmlStr;
}
//设置报表圆圈值
function SetStatistics(JsonSource) {
    var i = 1;
    var valueNum = 0;
    $.each(JsonSource, function (key, val) {
        valueNum = JsonSource[key].TestNum / JsonSource[key].QuestionNums * 100;
        if (0 < valueNum && valueNum <= 1) {
            valueNum = 1;
        }
        $('#indicatorContainer' + i).radialIndicator({
            barColor: '#87CEEB',
            barWidth: 20,
            initValue: valueNum,
            roundCorner: true,
            percentage: true
        });
        i++;
    });
}
//点击报表进入当前题目练习
function PraticeClick(cid) {
    window.location.replace("/Exercises/Pop_Practice?cid=" + cid);
}

