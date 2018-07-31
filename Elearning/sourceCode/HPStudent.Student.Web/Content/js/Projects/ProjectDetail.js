﻿$(function () {
    
    //分隔使用的技术
    var UseTechnologys = $("#UseTechnology").html();
    var arrayTech = UseTechnologys.split(",");
    $('#UseTechnology').empty();
    for (var techItem in arrayTech) {
        $('#UseTechnology').append('<li><a href="#"><span class="fa fa-tag"></span> ' + arrayTech[techItem] + '</a></li>');
    }

    //任务列表的文档、PPT和视频项添加事件
    $("#task-list").delegate(".btn-info", "click", function () {
        var ajaxUrl;
        var fileType = $(this).attr("data-type");

        if (fileType == "ppt") {
            //查看按钮
            ajaxUrl = "/Player/pdf?file=" + $(this).attr("data-src");
            openWin(ajaxUrl, fileType + "View");
        } else if (fileType == "video") {
            //测试按钮  
            ajaxUrl = "/Player/video?src=" + $(this).attr("data-src");
            openWin(ajaxUrl, fileType + "View");
        } else if (fileType == "url") {
            //测试按钮  
            ajaxUrl = $(this).attr("data-src");
            openWin(ajaxUrl, fileType + "View");
        } else if (fileType == "pdf") {
            //测试按钮  
            ajaxUrl = "/Player/Download?f=" + $(this).attr("data-src");
            window.open(ajaxUrl);
        }
        //openWin(ajaxUrl, fileType + "View");

        //alert(fileType + " : " + ajaxUrl);
        //在新窗口中打开PDF或者VIDEO
       
        //Ajax获取页面
        //$.ajax({
        //    type: "GET",
        //    url: ajaxUrl,
        //    success: function (html) {
        //        $("#pop_modaldialog").empty();
        //        $('#pop_modaldialog').append(html);
        //        $('#Task_Details').modal('show');
        //    },
        //    complete: function (html) {
        //        //修复icheck无法显示样式的bug
        //        $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
        //    }
        //});
    });


    //弹出确认窗口关闭 -- 取消
    $("#pop_modaldialog").delegate(".close", "click", function () {
        $('#Task_Details').modal('hide');
        //  $(this).parents(".message-box").removeClass("open");
    });
});