$(function () {
    var GID = getQueryStringByName("GID");
    var color = new Array("#A8FF24", "#FF0000", "#B15BFF", "#6A6AFF", "#2894FF", "#28FF28", "#F00000", "#FFC125", "#FF1493", "#FFFACD", "#FFE7BA", "#FFE1FF", "#FFD39B", "#FFBBFF", "#FFAEB9", "#FF8C69", "#FF8247", "#FF7256", "#FF6347", "#FF34B3", "#EEEED1", "#D1D1D1", "#CDC9A5", "#CAFF70");
    $.ajax({
        type: "post",
        url: "/QuestionGroup/GetShowResult",
        data: "GID=" + GID,
        success: function (data) {
            if (data.JsonAnswer == null || data.JsonAnswer == undefined || data.JsonAnswer == "") {
                $("#h1_title").html("无查询数据，请填写问卷后生成结果！");
                $("#h1_title").css("text-align", "center");
                $("#h1_title").css("padding-top", "200px");

            } else {

                $("#h1_title").html(data.Title);
                for (var i = 0; i < data.JsonAnswer.length; i++) {
                    if (data.JsonAnswer[i].Type == "radio" || data.JsonAnswer[i].Type == "checkbox") {
                        var HtmlRadioTemplate = '<div class="panel panel-default" >'
                                + '<div class="panel-heading">'
                                       + '<h4>' + data.JsonAnswer[i].Number + ' : ' + data.JsonAnswer[i].Title + '</h4>'
                                   + ' </div>'
                                   + ' <div class="panel-body" >'
                                           + ' <div id="' + data.JsonAnswer[i].Number + '" width="300px" height="300px"></div>'
                                   + ' </div>'
                               + ' </div>';
                        $("#questionAnswer_list").append(HtmlRadioTemplate);

                        pieLoad(data.JsonAnswer[i]);

                    }
                    else if (data.JsonAnswer[i].Type == "text" || data.JsonAnswer[i].Type == "textarea") {

                        var HtmlLi = '';
                        for (var j = 0; j < data.JsonAnswer[i].TxtAnswer.length; j++) {
                            HtmlLi += ' <li style="padding-top:8px"><label>' + data.JsonAnswer[i].TxtAnswer[j].Content + '</label></li>';
                        }


                        var HtmlTextTemplate = '<div class="panel panel-default" >'
                         + '<div class="panel-heading">'
                                + '<h4>' + data.JsonAnswer[i].Number + ' : ' + data.JsonAnswer[i].Title + '</h4>'
                            + ' </div>'
                            + ' <div class="panel-body">'
                                + ' <div style="overflow:scroll;overflow-x :auto;max-height:200px;"></div>'
                                + '<ul style="list-style:none;">'
                                   + HtmlLi
                                + ' </ul>'
                                + ' </div>'
                            + ' </div>'
                        + ' </div>';
                        $("#questionAnswer_list").append(HtmlTextTemplate);


                    }
                }
            
            }


        }



    });




})


function pieLoad(JsonAnswer) {
    var array = new Array();
    for (var i = 0; i < JsonAnswer.SelAnswer.length; i++) {
        array.push({ name: "" + JsonAnswer.SelAnswer[i].SelectContent + "", y: JsonAnswer.SelAnswer[i].SelectCount });
    }
    $('#' + JsonAnswer.Number + '').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: { text: '' },
        credits: { text: '' },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                }
            }
        },
        series: [{
            name: '选择数量',
            colorByPoint: true,
            data: array
        }]
    });

}