//数据初始化
var myDatatalbe = null; //初始化一个Datatable容器
var starPage; //起始页
var questionID = "";
var questionnumber = 0;
var checkID = "";
var checkNumber = 0;

//获得题目ID
function changeVar(ID)
{
    if(ID!="")
    {
        var arr = ID.split('Q');
        return parseInt(arr[1]);
    }
    return "";
  

}


(function ($) {
    var oldHTML = $.fn.html;
    $.fn.formhtml = function () {
        if (arguments.length) return oldHTML.apply(this, arguments);
        $("input,textarea,button", this).each(function () {
            this.setAttribute('value', this.value);
        });
        $(":radio,:checkbox", this).each(function () {
            if (this.checked) { this.setAttribute('checked', 'checked'); }
            else { this.removeAttribute('checked'); }
        });
        $("option", this).each(function () {
            if (this.selected) { this.setAttribute('selected', 'selected'); }
            else { this.removeAttribute('selected'); }
        });
        return oldHTML.apply(this);
    };
})(jQuery);



$(function () {
    var TID = getQueryStringByName("TID");
    if (TID > 0 && TID != undefined && TID != "") {
        GetIdByQuestionTemplate(TID);
    } else
    {
        AddBindTemplate();
    }
   
    
  

    DataBindTable(getRootPath() + "/QuestionTemplate/GetQuestionTemplateTable");




    //DataBindTable(getRootPath() +"/QuestionTemplate/GetQuestionTemplateTable");

    //$('#btnAddTemplate').on('click', function (e) {
    //    window.open('/QuestionTemplate/QuestionTemplateAdd');
    //});

});

function GetIdByQuestionTemplate(TID)
{
    
    var ajaxUrl = "/QuestionTemplate/GetIdByQuestionTemplate?TID=" + TID + "&" + Date.now();
    $.ajax({
        type: "POST",
        url: ajaxUrl,      
        success: function (data) {
            $("#getHtml").html(data.Content);
            $("#HID").val(data.TID);
            questionnumber = data.TopicCount;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown)
        {
            myAlert("提示", "模版获取失败！", "#pop_modaldialog");


        }
        //,
        //complete: function (html) {
        //    $('#QA_Select_Major').val($('#selMajor').find("option:selected").text());
        //    $('#QA_Select_Category').val($('#selCourse').find("option:selected").text());
        //    //修复icheck无法显示样式的bug
        //    $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
        //}
    });


}


function getQueryStringByName(name) {

    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

    if (result == null || result.length < 1) {

        return "";

    }

    return result[1];

}


$("#class-list").delegate(".btn-primary", "click", function () {

    if ($(this).attr("data-action") == "edit") {
        //编辑按钮
        window.location = "QuestionTemplateEdit?TID=" + $(this).attr("data-id");
        
    } else {
        myConfirm("确定要删除当前模版吗？", "模版删除后将无法恢复！", "DelQuestionTemplate(" + $(this).attr("data-id") + ")", "#pop_modaldialog");

        return false;
    }
});


//删除模版
function DelQuestionTemplate(val) {
    $.ajax({
        type: "post",
        url: "/QuestionTemplate/DeleteQuestionTemplate",
        dataType: "json",
        data: "TID=" + val,
        success: function (data) {
            //填充Datatable
            var CID = $('#selCourse').val();
            ajaxUrl = getRootPath() + "/QuestionTemplate/GetQuestionTemplateTable";
            //Datatable绑定数据
            DataBindTable(ajaxUrl);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAlert("提示", "模版删除失败！", "#pop_modaldialog");
        }
    });
    $("#pop_modaldialog").find(".message-box").removeClass("open");
}

//添加最终模版
function EditTemplate()
{
    var tid = $("#HID").val();
    var title = $("#txt_title").val();
    var content = $("#getHtml").formhtml();
    var topicCount = questionnumber;
   
    if (title != "" && title != undefined) {
        if (tid !="") {
            $.ajax({
                type: "post",
                url: "/QuestionTemplate/UpdateQuestionTemplate",
                dataType: "json",
                data: { tid: tid, title: title, content: content, topicCount: topicCount },
                success: function (data) {
                    if (data == 1) {
                        myAlert("提示", "模版修改成功！", "#pop_modaldialog");
                        $("#HID").val("");
                        $("#txt_title").val("");
                        $("#question_list").find("*[data-qid]").each(function () {
                            $(this).remove();
                            onresize();
                        });
                        questionnumber = 0;
                        window.location = "Index";
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    myAlert("提示", "模版修改失败！", "#pop_modaldialog");
                }
            });
        }else
        {

            $.ajax({
                type: "post",
                url: "/QuestionTemplate/AddQuestionTemplate",
                dataType: "json",
                data: { title: title, content: content, topicCount: topicCount },
                success: function (data) {

                    if (data == 1) {
                        myAlert("提示", "模版添加成功！", "#pop_modaldialog");
                        $("#HID").val("");
                        $("#txt_title").val("");
                        $("#question_list").find("*[data-qid]").each(function () {
                            $(this).remove();
                            onresize();
                        });
                        questionnumber = 0;
                        AddBindTemplate();
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    myAlert("提示", "模版添加失败！", "#pop_modaldialog");
                }
            });
        }
    } else
    {
        myAlert("提示", "请填写问卷调查标题！", "#pop_modaldialog");
    }
}


function GetLocalTime(nS) {
    nS = nS.replace("/Date(", "").replace(")/", "");

    var newDate = new Date(parseInt(nS));

    var year = newDate.getFullYear();
    var month = newDate.getMonth() + 1;
    var date = newDate.getDate();
    var hour = newDate.getHours();
    var minute = newDate.getMinutes();
    var second = newDate.getSeconds();
    return year + "-" + month + "-" + date;
}

  
function DataBindTable(ajaxUrl) {

    //if(sqlWhere!=null&&sqlWhere!=undefined&&sqlWhere!="")
    //{
    //    where += "";
    //}
    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_questiontemplate").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/Js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": ajaxUrl,
            "serverMethod": 'POST',
            "columns": [
	            { "data": "Title" },
                { "data": "UName" },
                { "data": "EditName" },
                { "data": "CreateDate" },
                { "data": "EditDate" },
                { "data": "Operation" }
            ],
            "columnDefs": [
                {
                    "targets": 3,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['CreateDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
                {
                    "targets": 4,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['EditDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                },
            ]
        });
        onresize();
    } else {
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl;
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();


    }


}


//自动生成QID
function GetQuestionID() {

    if (questionID == "") {

        return "Q" + (++questionnumber) + "";

    }
    return null;

}

//自动生成CID
function GetCheckID()
{

    if (checkID == "") {

        return "C" + (++checkNumber) + "";

    }
    return null;



}
   
//添加选项
function AddOptions(Options, QID) {
    var html = '';

    if (Options == 'radio') { 
        var CNAME = $("#" + QID + " li:last input").attr("name");
        var AddOptionsCount = $("#" + QID + " ul li").length;
        if (AddOptionsCount < 24) {
            //$("#" + QID).find("li").append(html);
            html = '<li style="padding-top:8px"><input type="radio" name="' + CNAME + '" />&nbsp;&nbsp;<input type="text" id="check_id4"  style="width:300px"  placeholder="选项"  /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>';
            //$("#" + QID).find("li").append(html);
            $("#" + QID + " li:last").append(html);
        } else
        {
            myAlert("提示", "添加选项不能超过24个！", "#pop_modaldialog");
        }

        
    }
    else {
        var AddOptionsCount = $("#" + QID + " ul li").length;
        if (AddOptionsCount < 24) {
            html = '  <li style="padding-top:8px"><input type="checkbox" id="' + GetCheckID() + '" name="rdo_checkbox" />&nbsp;&nbsp;<input type="text" id="check_id"  style="width:300px"  placeholder="选项"  /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
            $("#" + QID + " li:last").append(html);
        } else
        {
            myAlert("提示", "添加选项不能超过24个！", "#pop_modaldialog");
        }
    }


}

//绑定初始模版
function AddBindTemplate()
{
   
    var html = '';

    var QID = GetQuestionID();
    
    html = '<div class="panel panel-default" data-qid="' + QID + '"  QType="description">'
                      + ' <div class="panel-heading">'
                          + '<h4>' + QID + '</h4><h2 class="panel-title"><input type="text" style="width:800px"   placeholder="描述" /></h2>'
                           + '<ul class="panel-controls">'
                            + ' <li><a onclick="moveTopic(\'up\',\'radio\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'radio\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + '  <li><a  onclick="removeTopic(\'description\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                      + '</div>'
                  + ' </div>';
   

    for (var i = 0; i < 2; i++) {
        var ChooseQID = GetQuestionID();
        var CID = GetCheckID();
        html += '<div class="panel panel-default" data-qid="' + ChooseQID + '"  QType="radio">'
 + '<div class="panel-heading">'
                 + '<h4>' + ChooseQID + '</h4><h2 class="panel-title"><input type="text" name="txt_radio"  style="width:800px"      placeholder="单选题" /></h2>'
                 + ' <ul class="panel-controls">'
                   + ' <li><a onclick="moveTopic(\'up\',\'radio\',\'' + ChooseQID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'radio\',\'' + ChooseQID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                     + ' <li><a onclick="removeTopic(\'radio\',\'' + ChooseQID + '\')" class="panel-remove" ><span class="fa fa-times"></span></a></li>'
                 + ' </ul>'
             + ' </div>'
             + ' <div class="panel-body" id="' + ChooseQID + '">'
                 + ' <ul style="list-style:none;" >'
                     + ' <li style="padding-top:8px"><input type="radio" name="' + CID + '"/>&nbsp;&nbsp;<input type="text" id="check_id"  style="width:300px"  placeholder="选项"  /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
                     + ' <li style="padding-top:8px"><input type="radio" name="' + CID + '" />&nbsp;&nbsp;<input type="text" id="check_id2" style="width:300px"  placeholder="选项" /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
                     + ' <li style="padding-top:8px"><input type="radio" name="' + CID + '" />&nbsp;&nbsp;<input type="text" id="check_id3" style="width:300px"  placeholder="选项"  /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
                 + ' </ul>'
             + ' </div>'
                 + '<div class="panel-footer">'
                 + ' <button onclick="AddOptions(\'radio\',\'' + ChooseQID + '\')" class="btn btn-primary  pull-left">添加选项</button>'
                 + '</div>'
         + ' </div>';
       
    }
    html +='<div class="panel panel-default"  id="BtnBind">'
                 + '<div class="panel-footer">'
                 + ' <button onclick="EditTemplate()" style="width:150px" class="btn btn-primary pull-right">保存模版</button>'
                 + '</div>'
         + ' </div>';

    $("#question_list").append(html);

   
    onresize();


 
}

//删除题目
function removeTopic(Options, QID)
{
    var ID = $("[data-qid='" + QID + "']").find("H4").text();
    //if (ID=="Q1")
    //{
    //    $("[data-qid='" + QID + "']").remove();
    //}else{
    var changeID = changeVar(ID) + 1;
    $("[data-qid='" + QID + "']").remove();
    onresize();


    for (var i = changeID; i <= questionnumber; i++) {
        var downQID = "Q" + (i - 1);
       
        $("[data-qid='Q" + i + "']").find("H4").text()  ;
        $("[data-qid='Q" + i + "']").find("H4").text(downQID);



        $("[data-qid='Q" + i + "']").attr("data-qid", downQID);
        $("[data-qid='" + downQID + "']").find(".panel-body").attr("id", downQID);
       
        if (Options == "radio") {
            $("[data-qid='" + downQID + "']").find(".panel-controls li a").attr("onclick", "removeTopic(\'radio\','" + downQID + "')");
            $("[data-qid='" + downQID + "']").find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'radio\','" + downQID + "')");
        }
        else if (Options == "checkbox") {
            $("[data-qid='" + downQID + "']").find(".panel-controls li a").attr("onclick", "removeTopic(\'checkbox\','" + downQID + "')");
            $("[data-qid='" + downQID + "']").find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'checkbox\','" + downQID + "')");
        } else
        {
            $("[data-qid='" + downQID + "']").find(".panel-controls li a").attr("onclick", "removeTopic(\'notOptions\','" + downQID + "')");
        }

    }
    questionnumber--;
    
    }

//清除选项
function removeOptions(val)
{
    var optionsCount = val.parent().parent().parent().find("li").length;
    if (optionsCount > 1) {
        val.parent().remove();
    } else
    {
        myAlert("提示", "选项不能少于1个！", "#pop_modaldialog");

    }
}

function changeTitle(type, QID)
{
    if (type == 'school')
    {
         $("[data-qid='" + QID + "']").find("H2 input").val("学校");

    } else if (type == 'grade')
    {
        $("[data-qid='" + QID + "']").find("H2 input").val("班级");

    } else if (type == 'teacher')
    {
        $("[data-qid='" + QID + "']").find("H2 input").val("老师");
    }


}

//上下移动题目
function moveTopic(type, Options, QID)
{
    var move = $("[data-qid='" + QID + "']");
    if (type == 'up') {
        if (QID != "Q1") {

            var changeUp = move.find("H4").html();
            changeUp = changeUp.substring(1, changeUp.length);
            var upQID = "Q" + (parseInt(changeUp) - 1) + ""
            move.find("H4").html(upQID);
            move.attr("data-qid", upQID);
            move.find(".panel-body").attr("id", upQID);
            if (Options == "radio") {
                move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'radio\','" + upQID + "')");
                move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'radio\','" + upQID + "')");
                move.find(".fa-times").parent().attr("onclick", "removeTopic(\'radio\','" + upQID + "')");
                move.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'radio\','" + upQID + "')");
            }
            else if (Options == "checkbox") {
                move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'checkbox\','" + upQID + "')");
                move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'checkbox\','" + upQID + "')");
                move.find(".fa-times").parent().attr("onclick", "removeTopic(\'checkbox\','" + upQID + "')");
                move.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'checkbox\','" + upQID + "')");
            } else if (Options == "text") {
                var change = 0;
                move.find(".panel-footer .btn-primary").each(function () {
                    change = ++change;
                    if (change==1)
                    {
                        $(this).attr("onclick", "changeTitle(\'school\','" + upQID + "')");
                    } else if (change == 2)
                    {
                        $(this).attr("onclick", "changeTitle(\'grade\','" + upQID + "')");
                    } else if (change == 3)
                    {
                        $(this).attr("onclick", "changeTitle(\'teacher\','" + upQID + "')");
                    }
                });
                
                move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'text\','" + upQID + "')");
                move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'text\','" + upQID + "')");
                move.find(".fa-times").parent().attr("onclick", "removeTopic(\'notOptions\','" + upQID + "')");
                
            }
            else {
                move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'notOptions\','" + upQID + "')");
                move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'notOptions\','" + upQID + "')");
                move.find(".panel-controls li a").attr("onclick", "removeTopic(\'notOptions\','" + upQID + "')");
            }
            var prev = move.prev();
            move.after(prev);
            var changeDown = prev.find("H4").html();
            changeDown = changeDown.substring(1, changeDown.length);
            var downQID = "Q" + (parseInt(changeDown) + 1) + ""

            prev.find("H4").html(downQID);
            prev.attr("data-qid", downQID);
            prev.find(".panel-body").attr("id", downQID);
            if (Options == "radio") {
                prev.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'radio\','" + downQID + "')");
                prev.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'radio\','" + downQID + "')");
                prev.find(".fa-times").parent().attr("onclick", "removeTopic(\'radio\','" + downQID + "')");
                prev.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'radio\','" + downQID + "')");
            }
            else if (Options == "checkbox") {
                prev.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'radio\','" + downQID + "')");
                prev.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'radio\','" + downQID + "')");
                prev.find(".fa-times").parent().attr("onclick", "removeTopic(\'radio\','" + downQID + "')");
                prev.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'checkbox\','" + downQID + "')");
            } else if (Options == "text") {
                var change = 0;
                prev.find(".panel-footer .btn-primary").each(function () {
                    change = ++change;
                    if (change == 1) {
                        $(this).attr("onclick", "changeTitle(\'school\','" + downQID + "')");
                    } else if (change == 2) {
                        $(this).attr("onclick", "changeTitle(\'grade\','" + downQID + "')");
                    } else if (change == 3) {
                        $(this).attr("onclick", "changeTitle(\'teacher\','" + downQID + "')");
                    }
                });
                prev.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'text\','" + downQID + "')");
                prev.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'text\','" + downQID + "')");
                prev.find(".fa-times").parent().attr("onclick", "removeTopic(\'notOptions\','" + downQID + "')");
                
            }
            else {
                prev.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'notOptions\',\'radio\','" + downQID + "')");
                prev.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'notOptions\',\'radio\','" + downQID + "')");
                prev.find(".panel-controls li a").attr("onclick", "removeTopic(\'notOptions\','" + downQID + "')");
            }
        }

    } else if (type == 'down') {
      
        if (QID != "Q" + $("[data-qid]").length + "") {
           var changeDown = move.find("H4").html();
           changeDown = changeDown.substring(1, changeDown.length);
           var downQID = "Q" + (parseInt(changeDown) + 1) + ""
           move.find("H4").html(downQID);
           move.attr("data-qid", downQID);
           move.find(".panel-body").attr("id", downQID);
           if (Options == "radio") {
               move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'radio\','" + downQID + "')");
               move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'radio\','" + downQID + "')");
               move.find(".fa-times").parent().attr("onclick", "removeTopic(\'radio\','" + downQID + "')");
               move.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'radio\','" + downQID + "')");
           }
           else if (Options == "checkbox") {
               move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'radio\','" + downQID + "')");
               move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'radio\','" + downQID + "')");
               move.find(".fa-times").parent().attr("onclick", "removeTopic(\'radio\','" + downQID + "')");
               move.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'checkbox\','" + downQID + "')");
           } else if (Options == "text")
           {
               var change = 0;
               move.find(".panel-footer .btn-primary").each(function () {
                   change = ++change;
                   if (change == 1) {
                       $(this).attr("onclick", "changeTitle(\'school\','" + downQID + "')");
                   } else if (change == 2) {
                       $(this).attr("onclick", "changeTitle(\'grade\','" + downQID + "')");
                   } else if (change == 3) {
                       $(this).attr("onclick", "changeTitle(\'teacher\','" + downQID + "')");
                   }
               });
               move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'text\','" + downQID + "')");
               move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'text\','" + downQID + "')");
               move.find(".fa-times").parent().attr("onclick", "removeTopic(\'notOptions\','" + downQID + "')");
               


           }else {
               move.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'notOptions\','" + downQID + "')");
               move.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'notOptions\','" + downQID + "')");
               move.find(".panel-controls li a").attr("onclick", "removeTopic(\'notOptions\','" + downQID + "')");
           }
           var next = move.next();
           move.before(next);
           var changeUp = next.find("H4").html();
           changeUp = changeUp.substring(1, changeUp.length);
           var upQID = "Q" + (parseInt(changeUp) - 1) + ""
           next.find("H4").html(upQID);
           next.attr("data-qid", upQID);
           next.find(".panel-body").attr("id", upQID);
           if (Options == "radio") {
               next.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'radio\','" + upQID + "')");
               next.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'radio\','" + upQID + "')");
               next.find(".fa-times").parent().attr("onclick", "removeTopic(\'radio\','" + upQID + "')");
               next.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'radio\','" + upQID + "')");
           }
           else if (Options == "checkbox") {
               next.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'checkbox\','" + upQID + "')");
               next.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'checkbox\','" + upQID + "')");
               next.find(".fa-times").parent().attr("onclick", "removeTopic(\'checkbox\','" + upQID + "')");
               next.find(".panel-footer .pull-left").attr("onclick", "AddOptions(\'checkbox\','" + upQID + "')");
           } else if (Options == "text") {
               var change = 0;
               next.find(".panel-footer .btn-primary").each(function () {
                   change = ++change;
                   if (change == 1) {
                       $(this).attr("onclick", "changeTitle(\'school\','" + upQID + "')");
                   } else if (change == 2) {
                       $(this).attr("onclick", "changeTitle(\'grade\','" + upQID + "')");
                   } else if (change == 3) {
                       $(this).attr("onclick", "changeTitle(\'teacher\','" + upQID + "')");
                   }
               });
               next.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'up\',\'text\','" + upQID + "')");
               next.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'down\',\'text\','" + upQID + "')");
               next.find(".fa-times").parent().attr("onclick", "removeTopic(\'notOptions\','" + upQID + "')");
           }
           else {
               next.find(".fa-arrow-up").parent().attr("onclick", "moveTopic(\'notOptions\',\'checkbox\','" + upQID + "')");
               next.find(".fa-arrow-down").parent().attr("onclick", "moveTopic(\'notOptions\',\'checkbox\','" + upQID + "')");
               next.find(".panel-controls li a").attr("onclick", "removeTopic(\'notOptions\','" + upQID + "')");
           }
       }

        }
        onresize();
    }
   



//添加题目
function addTopic(type) {
    var QID = GetQuestionID();
    var CID = GetCheckID();
    var html = '';
    $("#BtnBind").remove();
    if (type=='radio')
    {
        //'<div class="panel panel-default" typeA="a">'
        html = '<div class="panel panel-default" data-qid="' + QID + '" QType="radio">'
            + '<div class="panel-heading">'
                            + '<h4>' + QID + '</h4><h2 class="panel-title"><input type="text" style ="width:800px" placeholder="单选题" /></h2>'
                            + ' <ul class="panel-controls">'
                             + ' <li><a onclick="moveTopic(\'up\',\'radio\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'radio\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + ' <li><a onclick="removeTopic(\'radio\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                        + ' </div>'
                        + ' <div class="panel-body" id="' + QID + '">'
                            + ' <ul style="list-style:none;" >'
                                + ' <li style="padding-top:8px"><input type="radio" name="' + CID + '"/>&nbsp;&nbsp;<input type="text"   style="width:300px"  placeholder="选项"  /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
                                + ' <li style="padding-top:8px"><input type="radio" name="' + CID + '" />&nbsp;&nbsp;<input type="text"  style="width:300px"  placeholder="选项" /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
                                + ' <li style="padding-top:8px"><input type="radio" name="' + CID + '" />&nbsp;&nbsp;<input type="text"  style="width:300px"  placeholder="选项"  /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'

                        + ' </div>'
                        + '<div class="panel-footer">'

                            + '<button class="btn btn-primary  pull-right">确定</button>'
                            + ' <button onclick="AddOptions(\'radio\',\'' + QID + '\')" class="btn btn-primary  pull-left">添加选项</button>'
                        + '</div>'
                    + ' </div>';



        $("#question_list").append(html);
       
    }
    else if (type == 'checkbox')
    {
        var lihtml = "";
        for (var i = 0; i < 3; i++) {
            lihtml += '  <li style="padding-top:8px"><input type="checkbox" id="' + GetCheckID() + '"  name="rdo_checkbox" />&nbsp;&nbsp;<input type="text" id="check_id"  style="width:300px" placeholder="选项" /><a onclick="removeOptions($(this))" class="panel-remove"><span class="fa fa-times"></span></a></li>'
        }
        html = '<div class="panel panel-default" data-qid="' + QID + '" QType="checkbox">'
        + '  <div class="panel-heading">'
                            + '<h4>' + QID + '</h4><h2 class="panel-title"><input type="text" style="width:800px"   placeholder="多选题" /></h2>'
                            + ' <ul class="panel-controls">'
                             + ' <li><a onclick="moveTopic(\'up\',\'checkbox\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'checkbox\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + '  <li><a   onclick="removeTopic(\'checkbox\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                            + '</div>'
                        + ' <div class="panel-body" id="' + QID + '">'
                            + '  <ul style="list-style:none;">'
                                + lihtml
                                + '</ul>'
                                + '</div>'
                                + ' <div class="panel-footer">'
                                + ' <button onclick="AddOptions(\'checkbox\',\'' + QID + '\')" class="btn btn-primary  pull-left">添加选项</button>'
                                + '</div>'
        + ' </div>';
                    
        $("#question_list").append(html);
    }
    else if(type=='text')
    {
        html = '<div class="panel panel-default" data-qid="' + QID + '" QType="text">'
        + '<div class="panel-heading">'
                                + '<h4>' + QID + '</h4><h2 class="panel-title"><input type="text" style="width:800px"  placeholder="单行填空" /></h2>'
                                + '<ul class="panel-controls">'
                                + ' <li><a onclick="moveTopic(\'up\',\'text\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'text\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                    + '<li><a onclick="removeTopic(\'notOptions\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                                + '</ul>'
                            + ' </div>'
                            + ' <div class="panel-body" id="' + QID + '">'
                                + '  <input type="text" id="' + CID + '" class="col-md-12" />'

                            + ' </div>'
                            + ' <div class="panel-footer">'
                            + '<button class="btn btn-primary"    onclick="changeTitle(\'school\',\'' + QID + '\')" pull-right">学校</button>'
                             + '&nbsp;<button class="btn btn-primary"    onclick="changeTitle(\'grade\',\'' + QID + '\')" pull-right">年级</button>'
                              + '&nbsp;<button class="btn btn-primary"   onclick="changeTitle(\'teacher\',\'' + QID + '\')" pull-right">老师</button>'
                            + ' </div>'
            + ' </div>'
        $("#question_list").append(html);
    }
    else if (type == 'textarea') {
        html = '<div class="panel panel-default" data-qid="' + QID + '"  QType="textarea">'
                        + ' <div class="panel-heading">'
                            + '<h4>' + QID + '</h4><h2 class="panel-title"><input type="text" style="width:800px"  placeholder="多行填空" /></h2>'
                            + '<ul class="panel-controls">'
                              + ' <li><a onclick="moveTopic(\'up\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + '  <li><a  onclick="removeTopic(\'notOptions\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                        + '</div>'
                        + '<div class="panel-body" id="' + QID + '">'
                            + '<textarea style="width:500px;height:200px" id="' + CID + '"></textarea>'
                        + ' </div>'
                        + ' <div class="panel-footer">'
                           
                        + ' </div>'
                    + ' </div>';
        $("#question_list").append(html);
    }
    else if (type == 'description')
    {
        html = '<div class="panel panel-default" data-qid="' + QID + '"  QType="description">'
                      + ' <div class="panel-heading">'
                          + '<h4>' + QID + '</h4><h2 class="panel-title"><input type="text" style="width:800px"  placeholder="描述" /></h2>'
                           + '<ul class="panel-controls">'
                           + ' <li><a onclick="moveTopic(\'up\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + '  <li><a  onclick="removeTopic(\'notOptions\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                      + '</div>'
                      + ' <div class="panel-footer">'
                        
                      + ' </div>'
                  + ' </div>';
        $("#question_list").append(html);

    } else if (type == 'name')
    {
    
        html = '<div class="panel panel-default" data-qid="' + QID + '"  QType="name">'
                        + ' <div class="panel-heading">'
                            + '<h4>' + QID + '</h4><h2 class="panel-title">姓名</h2>'
                            + '<ul class="panel-controls">'
                             + ' <li><a onclick="moveTopic(\'up\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + '  <li><a  onclick="removeTopic(\'notOptions\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                        + '</div>'
                        + '<div class="panel-body" id="' + QID + '">'
                        + ' <ul style="list-style:none;" >'
                            + '<li style="padding-top:8px"><input type="text" id="check_id"  class="col-md-12"  placeholder="请填写姓名"  /></li>'
                       + ' </ul>'
                        + ' </div>'
                        + ' <div class="panel-footer">'
                            
                        + ' </div>'
                    + ' </div>';
        $("#question_list").append(html);

    } else if (type == 'telephone') {

        html = '<div class="panel panel-default" data-qid="' + QID + '"  QType="telephone">'
                        + ' <div class="panel-heading">'
                            + '<h4>' + QID + '</h4><h2 class="panel-title">电话号码</h2>'
                            + '<ul class="panel-controls">'
                            + ' <li><a onclick="moveTopic(\'up\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + '  <li><a  onclick="removeTopic(\'notOptions\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                        + '</div>'
                        + '<div class="panel-body" id="' + QID + '">'
                        + ' <ul style="list-style:none;" >'
                            + '<li style="padding-top:8px"><input type="text" id="check_id"  class="col-md-12"  placeholder="请填写电话号码"  /></li>'
                       + ' </ul>'
                        + ' </div>'
                        + ' <div class="panel-footer">'
                           
                        + ' </div>'
                    + ' </div>';
        $("#question_list").append(html);

    } else if (type == 'email') {

        html = '<div class="panel panel-default" data-qid="' + QID + '"  QType="email">'
                        + ' <div class="panel-heading">'
                            + '<h4>' + QID + '</h4><h2 class="panel-title">电子邮箱</h2>'
                            + '<ul class="panel-controls">'
                            + ' <li><a onclick="moveTopic(\'up\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-up"></span></a></li>'
                                + ' <li><a onclick="moveTopic(\'down\',\'notOptions\',\'' + QID + '\')" class="panel-remove"><span class="fa fa-arrow-down"></span></a></li>'
                                + '  <li><a  onclick="removeTopic(\'notOptions\',\'' + QID + '\')"  class="panel-remove"><span class="fa fa-times"></span></a></li>'
                            + ' </ul>'
                        + '</div>'
                        + '<div class="panel-body" id="' + QID + '">'
                        + ' <ul style="list-style:none;" >'
                            + '<li style="padding-top:8px"><input type="text" id="check_id"  class="col-md-12"  placeholder="电子邮箱"  /></li>'
                       + ' </ul>'
                        + ' </div>'
                        + ' <div class="panel-footer">'
                           
                        + ' </div>'
                    + ' </div>';
        $("#question_list").append(html);

    } 
  
    html = '<div class="panel panel-default"  id="BtnBind">'
                 + '<div class="panel-footer">'
                 + ' <button onclick="EditTemplate()" style="width:150px" class="btn btn-primary  pull-right">保存模版</button>'
                 + '</div>'
         + ' </div>';
    $("#question_list").append(html);

    postion();
   
}

//新建题目页面自动下拉
function postion() {
    var h = document.body.clientHeight;
    var speed = 1000;
    var windowHeight = parseInt($("body").css("height"));
    $('html > body').animate({
        scrollTop: h + 'px'
    },
            speed);
}

