var myDatatalbe;
var messagesObj = {
};
$(function () {
    //设定活动背景标签
    $("#GetMessageIndexA").addClass("active");
    var MessagesObj = Object.create(messagesObj);
    BindGetMessageDatatable('/Message/GetMessageData', MessagesObj);
});
//绑定收件箱Datatable
function BindGetMessageDatatable(ajaxUrl, dataJson) {
    //清空Datatable
    $('#QA_SelectList').empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_question").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: "/content/js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": ajaxUrl,
                "data": dataJson
            },
            "serverMethod": 'POST',
            "columns": [
	            { "data": "Sender" },
                { "data": "Title" },
                { "data": "DateCreated" }
            ]
            ,
            "columnDefs": [
                {
                    "targets": 1, "render": function (data, type, row) {
                        return "<a href='#' onclick='ViewDetailsPage(" + row["MID"] + ")'>" + row["Title"] + "</a>";
                    }
                },
                 {
                     "targets": 2, "render": function (data, type, row) {

                         var javascriptDate = new Date(new Date(parseInt(data.substr(6))));
                         javascriptDate = javascriptDate.getFullYear() + "-" + (javascriptDate.getMonth() + 1) + "-" + javascriptDate.getDate();
                         return javascriptDate;
                     }
                 }]
        });
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = {
            "url": ajaxUrl,
            "data": dataJson
        };
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
    }
}
//显示接收邮件详情
function ViewDetailsPage(MID) {
    var ajaxUrl = getRootPath() + "/Message/ReaderGetMessageDetail?MID=" + MID;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#previewMessagesPanel').modal('show');
        },
        complete: function (html) {
        }
    });
}
//关闭收件详情
function ClosepreviewMessagesPanel() {
    window.location.reload();
}
//打开举报信息层
function OpenMessageReport(Tid) {
    var ajaxUrl = getRootPath() + "/MessageReport/MessageReport?Tid=" + Tid;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_MessageReportmodaldialog").empty();
            $('#pop_MessageReportmodaldialog').append(html);
            $('#MessageReportPanel').modal('show');
        },
        complete: function (html) {
            SendMessageReport();
        }
    });
}
//举报信息发送
function SendMessageReport() {
    var messageReportObj = {
        Tid: 0,
        Body: '',
        Category: 0
    };
    $("#MessageReportSend").click(function () {
        var MessageReportObj = Object.create(messageReportObj);
        MessageReportObj.Tid = $(this).attr("data-id");
        MessageReportObj.Body = $("#MessageReportContent").val();
        //执行异步添加
        $.ajax({
            type: "post",
            url: "/MessageReport/MessageReportSend",
            dataType: "json",
            data: MessageReportObj,
            success: function (data) {
                if (data.ResultState == 0) {
                    $('#MessageReportPanel').modal('hide');
                    $("#pop_MessageReportmodaldialog").empty();
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'success',
                    });
                } else {
                    noty({
                        text: data.ResultMsg,
                        layout: 'topRight',
                        type: 'fail',
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    })
}