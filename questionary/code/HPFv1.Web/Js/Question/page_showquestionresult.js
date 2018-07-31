/// <reference path="../actions.js" />
function getQueryStringByName(name) {

    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    

    if (result == null || result.length < 1) {

        return "";

    }

    return result[1];

}


$(function () {
    var QID = getQueryStringByName("QID");
      $.ajax({
        type: "post",
        url: "/Question/GetShowResult",
        data: "QID=" + QID,
        success: function (data)
        {
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
