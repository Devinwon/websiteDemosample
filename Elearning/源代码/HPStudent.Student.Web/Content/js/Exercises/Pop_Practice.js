$(function () {
    //加载题目
    SetTest(eval($("#testTitle").val())[0]);
    //赋予答题卡点击事件
    $(".ButtonNull").click(function () {
        var Nowrowid = $(this).val();
        //保存题目答案并把信息写入答题卡
        WriteAnswer(Nowrowid);
        var rowid = parseInt($(this).text());
        //查看是否超过加载题目，如果超过从数据库加载
        var JsonTestStr = eval($("#testTitle").val());
        if (rowid > JsonTestStr[JsonTestStr.length - 1].ROWID || rowid < JsonTestStr[0].ROWID) {
            GetTest(rowid);
        }
        else {
            $.each(JsonTestStr, function (key, val) {
                if (JsonTestStr[key].ROWID == rowid) {
                    SetTest(JsonTestStr[key]);
                    return false;
                }
            });
        }
        //页面置顶
        $('html,body').animate({ scrollTop: '0px' }, 800);
    })
    onresize();
})
//设置题目
function SetTest(JsonSource) {
    SetTitle(JsonSource.ROWID + ".题目：" + JsonSource.Title)
    SetAnswer(JsonSource);
    //设置按钮值以便获得题号
    SetButtonValue(JsonSource.ROWID);
    $("#hiddenQID").val(JsonSource.QID);
}
//设置题目标题
function SetTitle(title) {
    $("#HTitle").text(title);
}
//设置选项
function SetAnswer(JsonStr) {
    var arrAnswer = new Array();
    arrAnswer[0] = JsonStr.A;
    arrAnswer[1] = JsonStr.B;
    arrAnswer[2] = JsonStr.C;
    arrAnswer[3] = JsonStr.D;
    var arrOption = new Array();
    arrOption[0] = "A";
    arrOption[1] = "B";
    arrOption[2] = "C";
    arrOption[3] = "D";
    //判断是单选题还是复选题
    HtmlStr = "";
    //获得答题卡验证是否已经答题
    var answerCard = $("#btnAnswer" + JsonStr.ROWID);
    if (typeof (answerCard.attr("trueanswer")) != "undefined") {
        for (var i = 0; i < arrAnswer.length; i++) {
            HtmlStr += "<div class=\"form-group\">";
            HtmlStr += " <div class=\"col-md-12\">";
            HtmlStr += "<label class=\"check\"> " + arrOption[i] + "：" + arrAnswer[i] + "</label>";
            HtmlStr += "</div>";
            HtmlStr += "</div>";
        }
        var h3color = "red";
        if (answerCard.attr("istrue").toLowerCase() == "true") {
            h3color = "green";
        }
        HtmlStr += "<div class=\"form-group\">";
        HtmlStr += " <div class=\"col-md-12\">";
        HtmlStr += "<h3 style=\"color:" + h3color + "\">你的答案：" + answerCard.attr("youranswer");
        HtmlStr += "</h3>"
        HtmlStr += "</div>";
        HtmlStr += "</div>";
        HtmlStr += "<div class=\"form-group\">";
        HtmlStr += " <div class=\"col-md-12\">";
        HtmlStr += "<h3>正确答案：" + answerCard.attr("trueanswer");
        HtmlStr += "</h3>"
        HtmlStr += "</div>";
        HtmlStr += "</div>";
    }
    else {
        if (JsonStr.AnswerType > 1) {
            for (var i = 0; i < arrAnswer.length; i++) {
                HtmlStr += "<div class=\"form-group\">";
                HtmlStr += " <div class=\"col-md-12\">";
                HtmlStr += "<label class=\"check\"><input type=\"checkbox\" name=\"selected\"  class=\"icheckbox\" value=\"" + arrOption[i] + "\"/> " + arrOption[i] + "：" + arrAnswer[i] + "</label>";
                HtmlStr += "</div>";
                HtmlStr += "</div>";
            }
        }
        else {
            for (var i = 0; i < arrAnswer.length; i++) {
                HtmlStr += "<div class=\"form-group\">";
                HtmlStr += " <div class=\"checkbox col-md-12\">";
                HtmlStr += " <label>";
                HtmlStr += "<input type=\"radio\" class=\"icheckbox\" value=\"" + arrOption[i] + "\" name=\"iradio\" />";
                HtmlStr += " <span>" + arrOption[i] + "：" + arrAnswer[i] + "</span>";
                HtmlStr += " </label>";
                HtmlStr += "</div>";
                HtmlStr += "</div>";
            }
        }
    }
    $("#checkForm").empty();
    $("#checkForm").append(HtmlStr);
    feiCheckbox();
}
//绑定上下题按钮值
function SetButtonValue(RowID) {
    $("#NextButton").val(RowID);
    $("#UpButton").val(RowID);
}
//修复动态加载的Checkbox无法显示样式的BUG
var feiCheckbox = function () {
    if ($(".icheckbox").length > 0) {
        $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_square-blue', radioClass: 'iradio_square-blue' });
    }
}
//下一题按钮事件
$("#NextButton").click(function () {
    var rowid = $(this).val();
    var maxrowid = $("#MaxRowID").val();
    //保存题目答案并把信息写入答题卡
    WriteAnswer(rowid);
    if (parseInt(rowid) >= parseInt(maxrowid)) {
        NotyAlert("已经到最后一题", 'fail');
    }
    else {
        rowid++;
        //查看是否超过加载题目，如果超过从数据库加载
        var JsonTestStr = eval($("#testTitle").val());
        if (rowid > JsonTestStr[JsonTestStr.length - 1].ROWID) {
            GetTest(rowid);
        }
        else {
            $.each(JsonTestStr, function (key, val) {
                if (JsonTestStr[key].ROWID == rowid) {
                    SetTest(JsonTestStr[key]);
                    return false;
                }
            });
        }
    }
})
//上一题按钮事件
$("#UpButton").click(function () {
    //查看是否超过加载题目，如果超过从数据库加载
    var rowid = $(this).val();
    //保存题目答案并把信息写入答题卡
    WriteAnswer(rowid);
    if (parseInt(rowid) <= 1) {
        noty({
            text: "已经是第一题",
            layout: 'topRight',
            type: 'fail',
        });
    }
    else {
        rowid--;
        //查看是否超过加载题目，如果超过从数据库加载
        var JsonTestStr = eval($("#testTitle").val());
        if (rowid < JsonTestStr[0].ROWID) {
            GetTest(rowid);
        }
        else {
            $.each(JsonTestStr, function (key, val) {
                if (JsonTestStr[key].ROWID == rowid) {
                    SetTest(JsonTestStr[key]);
                    return false;
                }
            });
        }
    }
})
//从后台获取题目
function GetTest(RowID) {
    //ajax加载题目数据
    var QA_SelectViewModel = {
        CID: 0,
        RowID: 0
    };
    //封装数据
    var QA_SelectViewModelObj = Object.create(QA_SelectViewModel);
    QA_SelectViewModelObj.CID = getQueryString("cid");
    QA_SelectViewModelObj.RowID = RowID;
    $.ajax({
        type: "post",
        url: "/Exercises/Get_Test",
        dataType: "json",
        data: QA_SelectViewModelObj,
        success: function (data) {
            if (data != null) {
                //重新封装题目数据
                $("#testTitle").val(data);
                //加载题目
                SetTest(eval(data)[0]);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
//写入答案到数据库
function WriteAnswer(RowId) {
    var Answer_JsonModel = {
        QID: 0,
        RowID: 0,
        YourAnswer: "",
        CID: 0
    };
    //如果该题已经作答不重复记录
    var ishave = $("#btnAnswer" + RowId).attr("istrue");
    if (ishave != "" && ishave != null && typeof (ishave) != "undefined") {
        return;
    }
    //封装数据
    var answerStr = "";
    $("input[name^='selected']").each(function (i) {
        var isCheck = $(this).is(':checked');
        if (isCheck == true) {
            answerStr += $(this).val() + ","
        }
    });
    $("input[name^='iradio']").each(function (i) {
        var isCheck = $(this).is(':checked');
        if (isCheck == true) {
            answerStr += $(this).val() + ","
        }
    });
    if (answerStr == "") {
        return;
    }
    var Answer_JsonModelObj = Object.create(Answer_JsonModel);
    Answer_JsonModelObj.RowID = RowId;
    Answer_JsonModelObj.QID = $("#hiddenQID").val();
    Answer_JsonModelObj.YourAnswer = answerStr;
    var newcid = getQueryString("cid");
    $.ajax({
        type: "post",
        url: "/Exercises/SaveAnswer?NewCID=" + newcid,
        dataType: "json",
        data: Answer_JsonModelObj,
        success: function (data) {
            if (data.RowID != 0) {
                if (Answer_JsonModelObj.YourAnswer != data.YourAnswer) {
                    NotyAlert("该题已经作答过", 'fail');
                }
                else {
                    if (data.IsTrue == true) {
                        NotyAlert("回答正确", 'success');
                    }
                    else {
                        NotyAlert("回答错误", 'error');
                    }
                }
                //修改答题卡
                var btnAnswer = $("#btnAnswer" + data.RowID);
                btnAnswer.attr("istrue", data.IsTrue);
                btnAnswer.attr("yourAnswer", data.YourAnswer);
                btnAnswer.attr("trueAnswer", data.TrueAnswer);
                btnAnswer.attr("answeranalysis", data.AnswerAnalysis);
                var btncss = "btn-danger";
                //修改答题数量
                if (data.IsTrue == true) {
                    btncss = "btn-success";
                    $("#TrueI").text(parseInt($("#TrueI").text()) + 1);
                }
                else {
                    $("#FalseI").text(parseInt($("#FalseI").text()) + 1);
                }
                var sumarr = $("#SumI").text().split("/");
                $("#SumI").text(parseInt(sumarr[0]) + 1 + "/" + sumarr[1]);
                btnAnswer.addClass(btncss);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function NotyAlert(textstr, typestr) {
    noty({
        text: textstr,
        layout: 'topRight',
        type: typestr,
    });
}
//获取地址栏的值
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + escape(name) + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}