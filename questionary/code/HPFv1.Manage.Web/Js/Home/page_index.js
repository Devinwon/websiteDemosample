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
            Morris.Line({
                element: 'index-line-1',
                data: data,
                xkey: 'PostDate',
                ykeys: ['StudentNum', 'TeacherNum'],
                labels: ['活跃学生', '活跃教师'],
                resize: true,
                hideHover: true,
                gridTextSize: '10px',
                lineColors: ['#FF0000', '#33414E'],
                gridLineColor: '#E5E5E5'
            });
            /* EMD Line dashboard chart */
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });




});