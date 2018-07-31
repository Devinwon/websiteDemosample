//数据初始化
var myDatatalbe = null; //初始化一个Datatable容器
var starPage; //起始页
var checkNumber = 0;
var checkID = "";
var questionNumber = 0;


$(function () {

    DataBindTable(getRootPath() + "/Question/GetQuestionTable");


});


function GetLocalTime(nS) {
    nS = nS.replace("/Date(", "").replace(")/", "");

    var newDate = new Date(parseInt(nS));

    var year = newDate.getFullYear();
    var month = newDate.getMonth() + 1;
    var date = newDate.getDate();
    var hour = newDate.getHours();
    var minute = newDate.getMinutes();
    var second = newDate.getSeconds();
    if (month<10)
    {
        month = "0" + month;
    }
    if (date<10)
    {
        date = "0" + date;
    }


    return year + "-" + month + "-" + date ;
}


//绑定表格
function DataBindTable(ajaxUrl)
{
    var title = $("#txt_searchTitle").val();
    var startTime = $("#txt_startDate").val();
    var endTime = $("#txt_endDate").val();


    var jsonData = { title: title,startTime: startTime, endTime: endTime };
   
    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_question").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/Js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": ajaxUrl,
                "data": jsonData,
                "async": true
            },
            "serverMethod": 'POST',
            "columns": [
	           
                { "data": "Title" },
                { "data": "StartDate" },
                { "data": "EndDate" },
                { "data": "QNum" },
	            { "data": "Operation" },
            ],
            "columnDefs": [
                {
                    "targets": 1,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['StartDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
                {
                    "targets": 2,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['EndDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
            ]
        });
        onresize();
    } else
    {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = {
            "url": ajaxUrl,
            "data": jsonData,
             "async": true
        };
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();


    }


}


$("#btnSearchQuestion").click(function () {
    var startDate = $("#txt_startDate").val();
    var endDate = $("#txt_endDate").val();

    if (get_unix_time(startDate) > get_unix_time(startDate)) {
        noty({
            text: '开始时间不能大于结束时间',
            layout: 'topRight',
            type: 'fail',
        });
    } else
    {
        DataBindTable(getRootPath() + "/Question/GetQuestionTable");
    }

});




//操作
$("#class-list").delegate(".btn", "click", function () {

    var QID = $(this).attr("data-id");
    if ($(this).attr("data-action") == "edit") {
        //编辑按钮
        var ajaxUrl = getRootPath() + "/Question/QuestionEdit";
        $.ajax({
            type: "post",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-manager').modal('show');
            }
            , complete: function (data) {
                DatePickerLoad();
                QuestionBind("Update", getRootPath() + "/Question/QuestionBind?QID=" + QID);
            }

        });
    }
    else if ($(this).attr("data-action") == "delete") {
        myConfirm("确定要删除当前问卷吗？", "问卷删除后将无法恢复！", "DelQuestion(" + $(this).attr("data-id") + ")", "#pop_modaldialog");

        return false;
    }
    else if ($(this).attr("data-action") == "getData")
    {
        window.location = "/QuestionGroup/Index?QID=" + $(this).attr("data-id");
    
    }
    else if ($(this).attr("data-action") == "createQuestion")
    {
        GetCreateQuestion($(this).attr("data-id"));
    } else if ($(this).attr("data-action") == "createResult") {
        GetCreateResult($(this).attr("data-id"));
    } else if ($(this).attr("data-action") == "getHistory")
    {
        window.location = "/QuestionDetailHistory/Index?QID=" + $(this).attr("data-id");
    }

    else if ($(this).attr("data-action") == "ShowResult")
    {
        window.location = "ShowQuestionResult?QID=" + $(this).attr("data-id");
    }
});


var feDatepicker = function () {
    if ($(".datepicker").length > 0) {
        $(".datepicker").datepicker({ format: 'yyyy-mm-dd' });
        $("#dp-2,#dp-3,#dp-4").datepicker(); // Sample
    }

}// END Bootstrap datepicker

$("#btnAddQuestion").click(function () {
    var ajaxUrl = getRootPath() + "/Question/QuestionEdit";

    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');
        }
           , complete: function (data) {
               DatePickerLoad();
               QuestionBind("Add", getRootPath() + "/Question/QuestionBind?QID=0");
           }

    });



});



function QuestionBind(type, ajaxUrl)
{

    var useroption = '';
    var questionoption = '';
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (data) {
            for (var i = 0; i < data.QuestionTemplateList.length; i++) {
                useroption += '<option value=' + data.QuestionTemplateList[i].TID + '>' + data.QuestionTemplateList[i].Title + '</option>';
            }
            $("#sel_templates").append(useroption);

           
            if (type != "Add") {
                $("#txt_qid").val(data.QID);
                $("#txt_title").val(data.Title);
                $("#txt_description").val(data.Description);
                $("#txt_startTime").val(GetLocalTime(data.StartDate));
                $("#txt_endTime").val(GetLocalTime(data.EndDate));
                $("#sel_templates").val(data.TemplateID);
            } else {
                $("#txt_qid").val("");
                $("#txt_title").val("");
                $("#txt_description").val("");
                $("#txt_startTime").val("");
                $("#txt_endTime").val("");
            }

        }
    });


}


function EditQuestion()
{
    
    var qid = $("#txt_qid").val();
    var title = $("#txt_title").val();
    var tid = $("#sel_templates").val();
    var description = $("#txt_description").val();
    var startTime = $("#txt_startTime").val();
    var endTime = $("#txt_endTime").val();

    if (title == "" || title == undefined) {
        noty({
            text: '未填写标题',
            layout: 'topRight',
            type: 'fail',
        });
    }
    else if (description == "" || description == undefined)
    {
        noty({
            text: '未填写问卷描述内容',
            layout: 'topRight',
            type: 'fail',
        });
    } else if (startTime == "" || startTime == undefined && endTime == "" || endTime == undefined)
    {
        noty({
            text: '未填写开始时间或结束时间',
            layout: 'topRight',
            type: 'fail',
        });
    }
    else if (get_unix_time(startTime) > get_unix_time(endTime)) {
        noty({
            text: '开始时间不能大于结束时间',
            layout: 'topRight',
            type: 'fail',
        });
    }

    else if (tid == 0) {

        noty({
            text: '未选择问卷模版',
            layout: 'topRight',
            type: 'fail',
        });
       
    }else {
        if (qid != "") {
            $.ajax({
                type: "post",
                url: "/Question/UpdateQuestion",
                data: { qid: qid, title: title, description: description, startTime: startTime, endTime:endTime,tid: tid },
                success: function (data) {
                    if (data == 1) {
                        myAlert("提示", "问卷修改成功！", "#pop_modaldialog");
                        $('#edit-projectitem').modal('hide');
                        $("#pop_modaldialog").empty();
                        DataBindTable(getRootPath() + "/Question/GetQuestionTable");
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    myAlert("提示", "问卷修改失败！", "#pop_modaldialog");
                }
            });
        } else {
            $.ajax({
                type: "post",
                url: "/Question/AddQuestion",
                data: { title: title, description: description,startTime: startTime, endTime:endTime, tid: tid },
                success: function (data) {
                    if (data == 1) {
                        myAlert("提示", "问卷添加成功！", "#pop_modaldialog");
                        $('#edit-projectitem').modal('hide');
                        $("#pop_modaldialog").empty();
                        DataBindTable(getRootPath() + "/Question/GetQuestionTable");
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    myAlert("提示", "问卷添加失败！", "#pop_modaldialog");
                }
            });
        }
    }





}



//生成结果
function GetCreateResult(val)
{
    $.ajax({
        type: "post",
        url: "/Question/GetCreateResult",
        data: "QID=" + val,
        success: function (data) {
            if (data == 1) {
                myAlert("提示", "问卷结果生成成功！", "#pop_modaldialog");
                ajaxUrl = getRootPath() + "/Question/GetQuestionTable";
                //Datatable绑定数据
                DataBindTable(ajaxUrl);
            } else
            {
                myAlert("提示","问卷结果生成失败！","#pop_modaldialog");
            }
            

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAlert("提示", "问卷结果生成失败！", "#pop_modaldialog");
        }
    });
     
}



//生成问卷
function GetCreateQuestion(val)
{
    $.ajax({
        type: "post",
        url: "/Question/GetCreateQuestion",
        dataType: "json",
        data: "QID=" + val,
        success: function (data) {
            CreateQuestion(data.Content, val,data.Title);
            //填充Datatable
            //var CID = $('#selCourse').val();
            ajaxUrl = getRootPath() + "/Question/GetQuestionTable";
            //Datatable绑定数据
            DataBindTable(ajaxUrl);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAlert("提示", "问卷生成失败！", "#pop_modaldialog");
        }
    });
    $("#pop_modaldialog").find(".message-box").removeClass("open");

}

//自动生成CID
function GetCheckID() {

    if (checkID == "") {

        return "C" + (++checkNumber) + "";

    }
    return null;



}

//获得生成问卷
function CreateQuestion(val,qid,questionTitle)
{
    var questionHtml = '<div class="col-md-12" ><h2 style="text-align:center;">' + questionTitle + '</h2> ';
    var arr = new Array("A", "B", "C", "D", "E", "F", "E", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z");
    $(val).find("*[data-qid]").each(function () {
        var CID = GetCheckID();
        if ($(this).attr("QType")=="radio")
        {
            var html = '';
            var title = '';
            var Options = '';
            var QID = $(this).find("H4").html();
           
            var i = 0;
            title = '<h2 class="panel-title" style="text-align:center">' + $(this).find("h2").children().val() + '</h2>';
            var optionsCount = $(this).find(".panel-body li input:odd");
            $(this).find(".panel-body li input:odd").each(function () {
                var choice = arr[i++];
                Options += '<li style="padding-top:8px"><label>' + choice + '</label><input type="radio" value="' + choice + ':' + $(this).val() + '" name="' + CID + '"/>' + $(this).val() + '</li>'
            });
            html = ' '
            + '<div class="panel panel-default" QType="radio">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                + Options
                + ' </ul>'
                + '</div>'
                + '</div>';

            
            questionHtml += html;
        }
        else if ($(this).attr("QType") == "checkbox")
        {
            var html = ''
            var title = '';
            var Options = '';
            var QID = $(this).find("H4").html();
            var i = 0;
            title = '<h2 class="panel-title">' + $(this).find("h2").children().val() + '</h2>';
            var optionsCount = $(this).find(".panel-body li input:odd");
            $(this).find(".panel-body li input:odd").each(function () {
                var choice = arr[i++];
                Options += '<li style="padding-top:8px"><label>' + choice + '</label><input type="checkbox" value="' + choice + ':' + $(this).val() + '" id="' + GetCheckID() + '"/><lable>' + $(this).val() + '</lable></li>'
            });
            html = '<div class="panel panel-default" QType="checkbox">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                + Options
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;


        } else if ($(this).attr("QType") == "text")
        {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">' + $(this).find("h2").children().val() + '</h2>';
           
            html = '<div class="panel panel-default" QType="text">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '" >'
                + '<ul style="list-style:none;">'
                    + ' <li> <input type="text" id="' + CID + '" class="col-md-12" /></li> '
                + ' </ul>'
                + '</div>'
                + '</div>';
            questionHtml += html;

        
        }
        else if ($(this).attr("QType") == "textarea")
        {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">' + $(this).find("h2").children().val() + '</h2>';

            html = '<div class="panel panel-default" QType="textarea">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                    + ' <li><textarea class="col-md-12" style="height:200px" id="' + CID + '"></textarea> </li>'
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;
        }
        else if ($(this).attr("QType") == "description")
        {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title =  $(this).find("h2").children().val();

            html = '<div class="panel panel-default" QType="description">'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                    + '<li>'+title+'</li>'
                + ' </ul>'
                + '</div>'
            + '</div>';
            questionHtml += html;
            
        } else if ($(this).attr("QType") == "name")
        {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">姓名</h2>';

            html = '<div class="panel panel-default" QType="name">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                       + '<li style="padding-top:8px"><input type="text" id="' + GetCheckID() + '"  class="col-md-12"  placeholder="请填写姓名"  /></li>'
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;
        
        } else if ($(this).attr("QType") == "telephone")
        {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">电话</h2>';

            html = '<div class="panel panel-default" QType="telephone">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                       + '<li style="padding-top:8px"><input type="text" id="' + GetCheckID() + '"   class="col-md-12"  placeholder="请填写电话号码"  /></li>'
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;

        } else if ($(this).attr("QType") == "email") {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">电子邮箱</h2>';

            html = '<div class="panel panel-default" QType="email">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                       + '<li style="padding-top:8px"><input type="text" id="' + GetCheckID() + '"   class="col-md-12"  placeholder="请填写电子邮箱"  /></li>'
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;

        } else if ($(this).attr("QType") == "grade") {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">班级</h2>';

            html = '<div class="panel panel-default" QType="grade">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                       + '<li style="padding-top:8px"><input type="text" id="' + GetCheckID() + '"   class="col-md-12"  placeholder="请填写班级"  /></li>'
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;

        }
        else if ($(this).attr("QType") == "teacher") {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">老师</h2>';

            html = '<div class="panel panel-default" QType="teacher">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                       + '<li style="padding-top:8px"><input type="text" id="' + GetCheckID() + '"   class="col-md-12"  placeholder="请填写代课老师"  /></li>'
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;

        }
        else if ($(this).attr("QType") == "school") {
            var html = '';
            var title = '';
            var Options = '';
            var i = 0;
            var QID = $(this).find("H4").html();
            title = '<h2 class="panel-title">学校</h2>';

            html = '<div class="panel panel-default" QType="school">'
            + '<div class="panel-heading">'
            + title
            + '</div>'
            //+ ' <ul class="panel-controls"> </ul>'
                + '<div class="panel-body" id="' + QID + '">'
                + '<ul style="list-style:none;">'
                       + '<li style="padding-top:8px"><input type="text" id="' + GetCheckID() + '"  class="col-md-12"  placeholder="请填写代课老师"  /></li>'
                + ' </ul>'
                + '</div>'
                + '</div>';

            questionHtml += html;

        }
    });
    questionHtml += '<div class="panel panel-default">'
                        +'<div class="panel-body ">'
                            + '<div class="row" style="text-align:center">'
                                + ' <button id="btn_Commit" class="btn btn-primary " type="button"  onclick="QuestionCommit()" >确定</button>'
                            + ' </div>'
                        + ' </div>'
                    + ' </div>';
                    
    $.ajax({
        type: "post",
        url: "/Question/CreateQuestion",
        dataType: "json",
        data: { qid: qid, questionHtml: questionHtml},
        success: function (data) {
            if (data == 1) {
                myAlert("提示", "问卷生成成功！", "#pop_modaldialog");
            } else
            {
                myAlert("提示", "问卷生成失败，请检查数据是否已填写！", "#pop_modaldialog");
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            myAlert("提示", "问卷生成失败，请检查数据是否已填写！", "#pop_modaldialog");
        }
    });
  

}



//删除问卷
function DelQuestion(val) {
    $.ajax({ 
        type: "post",
        url: "/Question/DeleteQuestion",
        dataType: "json",
        data: "QID=" + val,
        success: function (data) {
            //填充Datatable
            ajaxUrl = getRootPath() + "/Question/GetQuestionTable";
            //Datatable绑定数据
            DataBindTable(ajaxUrl);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAlert("提示", "问卷删除失败！", "#pop_modaldialog");
        }
    });
    $("#pop_modaldialog").find(".message-box").removeClass("open");
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

function DatePickerLoad() {
    if ($(".datepicker").length > 0) {
        $(".datepicker").datepicker({ format: 'yyyy-mm-dd' });
        $("#dp-2,#dp-3,#dp-4").datepicker(); // Sample
    }

}

function get_unix_time(dateStr) {
    var newstr = dateStr.replace(/-/g, '/');
    var date = new Date(newstr);
    var time_str = date.getTime().toString();
    return time_str.substr(0, 10);
}