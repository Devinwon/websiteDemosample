
var myDatatalbe = null;


$(function () {
    var QID = getQueryStringByName("QID");
    $.ajax({
        type: "post",
        url: getRootPath() + "/QuestionDetailHistory/GetGroupList?QID=" + QID,
        success: function (data) {
            $('#sel_group').empty();
            $('#sel_group').append("<option value='0'>== 请选择 ==</option>");
            $.each(data, function (i, item) {
                $('#sel_group').append("<option value='" + item.GID + "'>" + item.GroupName + "</option>");
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAlert("提示", "分组绑定失败！", "#pop_modaldialog");
        }
    });

    DataBindTable(getRootPath() + "/QuestionDetailHistory/GetQuestionDetailHistoryTable");


});




//绑定表格
function DataBindTable(ajaxUrl) {
    var QID = getQueryStringByName("QID");
    var GID = $("#sel_group").val();
    var startTime = $("#txt_startDate").val();
    var endTime = $("#txt_endDate").val();
    var jsonData = { QID: QID, GID: GID, startTime: startTime, endTime: endTime };
    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_questiondetailhistory").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/Js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax":{ 
                "url":ajaxUrl,
                "data": jsonData
            },
            "serverMethod": 'POST',
            "columns": [
                { "data": "Title" },
                { "data": "GroupName" },
                { "data": "PostDate" },
                { "data": "PostIP" },
                { "data": "Operation" }
            ],
            "columnDefs": [
                {
                    "targets": 2,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['PostDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
            ]
        });
        onresize();
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = {
            "url": ajaxUrl,
            "data": jsonData
        };
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();


    }


}


$("#btnSearchHistory").click(function () {

    var startDate = $("#txt_startDate").val();
    var endDate = $("#txt_endDate").val();

    if (get_unix_time(startDate) > get_unix_time(endDate)) {
        noty({
            text: '开始时间不能大于结束时间',
            layout: 'topRight',
            type: 'fail',
        });
    } else {
        DataBindTable(getRootPath() + "/QuestionDetailHistory/GetQuestionDetailHistoryTable");
    }
   
});


//操作
$("#class-list").delegate(".btn-primary", "click", function () {

    var DID = $(this).attr("data-id");
    var QID = $(this).attr("data-qid");
    if ($(this).attr("data-action") == "edit") {
        //编辑按钮
        var ajaxUrl = getRootPath() + "/QuestionDetailHistory/ShowQuestionDetailHistory";
        $.ajax({
            type: "post",
            url: ajaxUrl,
            success: function (html) {               
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-manager').modal('show');
            }
            , complete: function () {
                GetQuestionTemplateBind(QID,DID);
            }

        });

    }
});




 
function GetQuestionTemplateBind(QID, DID)
{
    var ajaxUrl = getRootPath() + "/QuestionDetailHistory/GetQuestionTemplateBind";
    $.ajax({
        type: "post",
        url: ajaxUrl,
        data: "QID=" + QID,
        success: function (data) {
            $("#h1_title").html(data.Title);
            $("#getHtml").html(data.QuestionHtml);
          
        }, complete: function ()
        {
            QuestionDetailHistoryBind(DID);
        }
    });
}

function QuestionDetailHistoryBind(DID)
{
    
    var ajaxUrl = getRootPath() + "/QuestionDetailHistory/QuestionDetailHistoryBind";
    $.ajax({
        type: "post",
        url: ajaxUrl,
        data: "DID=" + DID,
        success: function (data) {
            var answers = $.parseJSON(data.Answer);
            $(".panel.panel-default").each(function () {     
                for (var i = 0; i < answers.length; i++) { 
                    if ($(this).find(".panel-body").attr("id") == answers[i].number) {
                        if (answers[i].type == "radio") {
                            $(this).find("li").each(function () {
                                if ($(this).find("label").html() == answers[i].choice)
                                {
                                    $(this).find("input").attr("checked", true);
                                }

                            });

                        } else if (answers[i].type == "checkbox") {

                            $(this).find("li").each(function () {
                                var array = answers[i].choice.split(",");
                               
                                for (var j = 0; j < array.length; j++) {
                                    if ($(this).find("label").html() == array[j]) {
                                        $(this).find("input").attr("checked", true);
                                    }
                                }
                                
                            });


                        } else if (answers[i].type == "text") {
                            $(this).find("input").attr("value", answers[i].Content);

                        } else if (answers[i].type == "textarea") {
                            $(this).find("textarea").html(answers[i].Content);
                        } else 
                        {
                            $(this).find("input").attr("value", answers[i].Content);
                        }

                    }
                 }

                });

                
            
        }

    });
}

function QuestionCommit()
{
    $("#pop_modaldialog").empty();

}


//弹出确认窗口关闭 -- 取消
$("#pop_modaldialog").delegate(".pop-confirm-warning-sure", "click", function () {
    $(this).parents(".message-box").removeClass("open");
});

//弹出确认窗口关闭  -- 确定
$("#pop_modaldialog").delegate(".pop-confirm-warning-close", "click", function () {
    $(this).parents(".message-box").removeClass("open");
    // alert('2');
});
