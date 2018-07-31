

$(function () {


});


function QuestionCommit()
{
 
    var jsonResult = Validation();
    if (jsonResult != false) {
        var qid = $("#QID").val();
        var gid = $("#GID").val();
        var qcode = GetQueryString("QuestionCode");
        $.ajax({
            type: "post",
            url: "/QuestionDetail/AddQuestionDetail",
            dataType: "json",
            data: { qid: qid, gid: gid, answer: jsonResult, qcode: qcode },
            success: function (data) {
                if (data >0) {
                    window.location = "/ErrorAlert/Index";
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                
                myAlert("提示", "调查提交失败！", "#pop_modaldialog");
            }
        });
    }

          
   
}
   

function Validation()
{

    var flag = true;
    var jsonResult = "[";

    $("#getHtml").find(".panel").each(function () {


        var resultJson = "";  //返回Json字符串
        var QID = "";

        if ($(this).attr("QType") == "radio") {
            var content = "";
            var choice = "";
            var radName = $(this).find(".panel-body ul li input").attr("name");

            $(this).find(".panel-body li").each(function () {
                if ($(this).find("input").prop("checked") == true) {
                    content = $(this).find("input").val();
                    choice = $(this).find("label").html();
                }
            });
            if (content != null && content != undefined && content != "") {
                QID = $(this).find(".panel-body").attr("ID");
                var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","choice":"' + choice + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                jsonResult += strJson;
            } else
            {  
                noty({
                    text: '问卷还未完成哦！请检查是否有未填题目！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;
            }
           



        }
        else if ($(this).attr("QType") == "checkbox") {
            var content = "{ ";
            choice = "";
            var index = 0;
            $(this).find(".panel-body li").each(function () {
                var CID = $(this).find("input").attr("ID");
                if ($(this).find("#" + CID + "").prop("checked") == true) {
                    choice += $(this).find("label").html() + ',';
                    content += '"' + $(this).find("label").html() + '":"' + $(this).find("#" + CID + "").val() + '",';
                }
            });
            content = content.substring(0, content.length - 1);
            content += "}"
            choice = choice.substring(0, choice.length - 1);

            if (content != null && content != undefined && content != "") {
                QID = $(this).find(".panel-body").attr("ID");
                var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","choice":"' + choice + '","title":"' + $(this).find("h2").html() + '","CheckContent":' + content + '},';
                jsonResult += strJson;
            } else
            {
                noty({
                    text: '问卷还未完成哦！请检查是否有未填题目！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;
            }

            

        }
        else if ($(this).attr("QType") == "text") {
            var content = $(this).find("input").val();
            if (content != null && content != undefined && content != "") {
                QID = $(this).find(".panel-body").attr("ID");
                var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                jsonResult += strJson;
            } else
            {
                noty({
                    text: '问卷还未完成哦！请检查是否有未填题目！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;
            }

          
        }
        else if ($(this).attr("QType") == "textarea") {
            var content = $(this).find("textarea").val();
            if (content != null && content != undefined && content != "")
            {
                QID = $(this).find(".panel-body").attr("ID");
                var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                jsonResult += strJson;
            } else
            {
                noty({
                    text: '问卷还未完成哦！请检查是否有未填题目！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;
            }
           

        }
        else if ($(this).attr("QType") == "name") {
            var content = $(this).find("input").val();
            if (content != null && content != undefined && content != "") {
                if (!content.match(/^[\u4e00-\u9fa5]+$/)) {  
                    noty({
                        text: '不能输入中文以外的字符哦！',
                        layout: 'topRight',
                        type: 'error',
                    });
                    flag = false;
                    return false;
                } else {
                    QID = $(this).find(".panel-body").attr("ID");
                    var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                    jsonResult += strJson;
                }

            } else {
                noty({
                    text: '姓名没有填写哦！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;

            }



        }
        else if ($(this).attr("QType") == "telephone") {
            var content = $(this).find("input").val();
            if (content != null && content != undefined && content != "") {
                if (!content.match(/^(((13[0-9]{1})|(14[0-9]{1})|(17[0]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/)) {
                    noty({
                        text: '电话号码格式不正确哦！',
                        layout: 'topRight',
                        type: 'error',
                    });
                    flag = false;
                    return false;
                } else {
                    QID = $(this).find(".panel-body").attr("ID");
                    var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                    jsonResult += strJson;
                }
            } else {
                noty({
                    text: '电话号码没有填写哦！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;

            }



        }
        else if ($(this).attr("QType") == "email") {
            var content = $(this).find("input").val();
            if (content != null && content != undefined && content != "") {
                if (!content.match(/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/)) {
                    noty({
                        text: '电子邮箱格式不正确哦！',
                        layout: 'topRight',
                        type: 'error',
                    });
                    flag = false;
                    return false;
                } else {
                    QID = $(this).find(".panel-body").attr("ID");
                    var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                    jsonResult += strJson;
                }

            } else {
                noty({
                    text: '电子邮箱还没有填写哦！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;
            }



        } else if ($(this).attr("QType") == "grade") {
            var content = $(this).find("input").val();
            if (content != null && content != undefined && content != "") {
                if (!content.match(/^[0-9A-Za-z\u4e00-\u9fa5]{2,14}$/)) {
                    noty({
                        text: '不能输入中文数字英文以外的字符哦！',
                        layout: 'topRight',
                        type: 'error',
                    });
                    flag = false;
                    return false;
                } else {
                    QID = $(this).find(".panel-body").attr("ID");
                    var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                    jsonResult += strJson;
                }

            } else {
                noty({
                    text: '班级没有填写哦！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;

            }



        } else if ($(this).attr("QType") == "teacher") {
            var content = $(this).find("input").val();
            if (content != null && content != undefined && content != "") {
                if (!content.match(/^[\u4e00-\u9fa5]+$/)) {
                    noty({
                        text: '不能输入中文以外的字符哦！',
                        layout: 'topRight',
                        type: 'error',
                    });
                    flag = false;
                    return false;
                } else {
                    QID = $(this).find(".panel-body").attr("ID");
                    var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                    jsonResult += strJson;
                }

            } else {
                noty({
                    text: '老师没有填写哦！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;

            }



        } else if ($(this).attr("QType") == "school") {
            var content = $(this).find("input").val();
            if (content != null && content != undefined && content != "") {
                if (!content.match(/^[0-9A-Za-z\u4e00-\u9fa5]{2,14}$/)) {
                    noty({
                        text: '不能输入中文数字英文以外的字符哦！',
                        layout: 'topRight',
                        type: 'error',
                    });
                    flag = false;
                    return false;
                } else {
                    QID = $(this).find(".panel-body").attr("ID");
                    var strJson = '{"number":"' + QID + '","type":"' + $(this).attr("QType") + '","title":"' + $(this).find("h2").html() + '","Content":"' + content + '"},';
                    jsonResult += strJson;
                }

            } else {
                noty({
                    text: '学校没有填写哦！',
                    layout: 'topRight',
                    type: 'error',
                });
                flag = false;
                return false;

            }



        }


    });
    jsonResult = jsonResult.substring(0, jsonResult.length - 1);
    jsonResult += "]";

    if (flag == true) {
        return jsonResult;
    } else
    {
        return flag;
    }
}





    function QuestionDetailLogin()
    {
        var qcode = GetQueryString("QuestionCode");
        var password = $("#txt_PassWord").val();
    
        if (password != "" && password != null && password != undefined) {
            $.ajax({
                type: "post",
                url: "/QuestionDetail/QuestionDetailLogin",
                dataType: "json",
                data: { qcode: qcode, password: password },
                success:function(data)
                {
                    if (data != "" && data != null && data != undefined) {
                        if (data.IsCraete != 0) {
                            if (data.IsResult == 0) {
                                $("#QID").val(data.QID);
                                $("#GID").val(data.GID);
                                $("#getHtml").html(data.QuestionHtml);
                                //2016年5月27日 涂建 替换getHtml
                                $("#getHtml").removeClass("error-container");
                                $("#getHtml").addClass("page-container2");
                            } else {

                                $("#getHtml").html('<h1 style="text-align:center;padding-top:200px">你好，本次调查已完成，谢谢登录！</h1>');
                            }
                        } else
                        {
                            $("#getHtml").html('<h1 style="text-align:center;padding-top:200px">问卷未生成，请先生成再登录！</h1>');
                        }

                    }
                    else {
                        myAlert("提示", "登录失败，请检查登录地址或密码！", "#pop_modaldialog");
                    }
                    
            
                },
                error: function (XMLHttpRequest, textStatus, errorThrown)
                {;
                    myAlert("提示", "登录失败，请检查登录地址或密码！", "#pop_modaldialog");
                }
            });    
        }
        else
        {
            myAlert("提示", "登录失败，请检查登录地址或密码！", "#pop_modaldialog");
        }
    
    }




