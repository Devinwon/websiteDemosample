var myDatatalbe = null;

$(function () {
   
    
    DataBindTable(getRootPath() + "/QuestionGroup/GetQuestionGroupTable");
    

});


//绑定表格
function DataBindTable(ajaxUrl) {

    //if(sqlWhere!=null&&sqlWhere!=undefined&&sqlWhere!="")
    //{
    //    where += "";
    //}
    var QID = getQueryStringByName("QID");
    $("#QA_SelectList").empty();
    if (myDatatalbe == null) {
        myDatatalbe = $(".datatable_questiongroup").dataTable({
            ordering: false,
            info: false,
            lengthChange: false,
            searching: false,
            language: {
                url: getRootPath() + "/Js/plugins/datatables/Chinese.js"
            },
            "processing": true,
            "serverSide": true,
            "ajax": ajaxUrl + "?QID=" + QID + "",
            "serverMethod": 'POST',
            "columns": [
	            { "data": "Title" },
                { "data": "UName" },
                { "data": "GroupName" },
                { "data": "Password" },
                { "data": "CreateDate" },
                { "data": "Operation" }
            ],
            "columnDefs": [
                {
                    "targets": 4,
                    "class": "text-center",
                    "render": function (data, type, row) {
                        var postdate = row['CreateDate'];
                        var outHtml = GetLocalTime(postdate);
                        return outHtml;
                    }
                }
            ]
        });
        onresize();
    } else {
        var QID = getQueryStringByName("QID");
        var oSettings = myDatatalbe.fnSettings();
        oSettings.ajax = ajaxUrl+ "?QID=" + QID + "";
        myDatatalbe.fnClearTable(0);
        myDatatalbe.fnDraw();
        onresize();


    }


}


function QuestionGroupBind(type,ajaxUrl)
{
    var useroption = '';
    var questionoption = '';
    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (data) {
            for (var i = 0; i < data.UsersList.length; i++) {
                useroption += '<option value=' + data.UsersList[i].UID + '>' + data.UsersList[i].NickName + '</option>';
            }
            $("#sel_users").append(useroption);

            for (var i = 0; i < data.QuestionList.length; i++) {
                questionoption += '<option value=' + data.QuestionList[i].QID + '>' + data.QuestionList[i].Title + '</option>';
            }
            $("#sel_question").append(questionoption);
            if (type != "Add") {
                $("#txt_gid").val(data.GID);
                $("#txt_groupName").val(data.GroupName);
                $("#txt_passWord").val(data.Password);
                $("#txt_passWord").attr("readonly", "readonly");
            } else
            {
                $("#txt_gid").val("");
                $("#txt_groupName").val("");
                $("#txt_passWord").val("");
            }
            
        }
    });



}


$("#btnAddQuestionGroup").click(function () {
    var ajaxUrl = getRootPath() + "/QuestionGroup/QuestionGroupEdit";

    $.ajax({
        type: "post",
        url: ajaxUrl,
        success: function (html) {
            $("#pop_modaldialog").empty();
            $('#pop_modaldialog').append(html);
            $('#edit-manager').modal('show');
        }
           , complete: function (data) {
               QuestionGroupBind("Add",getRootPath() + "/QuestionGroup/QuestionGroupBind?GID=0");
           }

    });



});







//操作
$("#class-list").delegate(".btn-primary", "click", function () {

    var GID = $(this).attr("data-id");
    if ($(this).attr("data-action") == "edit") {
        //编辑按钮
        var ajaxUrl = getRootPath() + "/QuestionGroup/QuestionGroupEdit";
        $.ajax({
            type: "post",
            url: ajaxUrl,
            success: function (html) {
                $("#pop_modaldialog").empty();
                $('#pop_modaldialog').append(html);
                $('#edit-manager').modal('show');
            }
            , complete: function (data)
            {
                QuestionGroupBind("Update", getRootPath() + "/QuestionGroup/QuestionGroupBind?GID=" + GID);
            }
            
        });

    } else if ($(this).attr("data-action") == "delete") {
        myConfirm("确定要删除当前分组吗？", "问卷删除后将无法恢复！", "DelQuestionGroup(" + GID + ")", "#pop_modaldialog");

        return false;
    } else
    {
        window.location = "ShowGroupResult?GID=" + $(this).attr("data-id");
    }
});


function passwordVerification(password,qid)
{
    var flag = false;
    $.ajax({
        type: "post",
        url: "/QuestionGroup/passwordVerification",
        data: { password: password, qid: qid },
        async: false,
        success: function (data) {
            if (data == 0) {
                flag = false;
            } else
            {
                flag = true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            flag = true;
        }
    });
    return flag;

}


function EditQuestionGroup()
{

    var gid = $("#txt_gid").val();
    var groupname = $("#txt_groupName").val();
    var password = $("#txt_passWord").val();
    var qid = getQueryStringByName("QID");
    var pVeri = passwordVerification(password, qid);

    if (groupname == "" || groupname == undefined) {
        noty({
            text: '未填写分组名称',
            layout: 'topRight',
            type: 'fail',
        });
    }
    else if (password == "" || groupname == undefined)
    {
        noty({
            text: '未填写分组密码',
            layout: 'topRight',
            type: 'fail',
        });
    } 
    else {
        if (gid != "") {
            $.ajax({
                type: "post",
                url: "/QuestionGroup/UpdateQuestionGroup",
                data: { gid: gid, groupname: groupname, password: password, qid: qid },
                success: function (data) {
                    if (data == 1) {
                        myAlert("提示", "分组修改成功！", "#pop_modaldialog");
                        $('#edit-projectitem').modal('hide');
                        $("#pop_modaldialog").empty();
                        //myDatatalbe = null;
                        DataBindTable(getRootPath() + "/QuestionGroup/GetQuestionGroupTable");
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    myAlert("提示", "分组修改失败！", "#pop_modaldialog");
                }
            });
        } else {
            if (pVeri) {
                noty({
                    text: '分组密码重复',
                    layout: 'topRight',
                    type: 'fail',
                });
            } else
            {
                $.ajax({
                    type: "post",
                    url: "/QuestionGroup/AddQuestionGroup",
                    data: { groupname: groupname, password: password, qid: qid },
                    success: function (data) {
                        if (data == 1) {
                            myAlert("提示", "分组添加成功！", "#pop_modaldialog");
                            $('#edit-projectitem').modal('hide');
                            $("#pop_modaldialog").empty();
                            //myDatatalbe = null;
                            DataBindTable(getRootPath() + "/QuestionGroup/GetQuestionGroupTable");
                        }

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        myAlert("提示", "分组添加失败！", "#pop_modaldialog");
                    }
                });

            }
            
        }
    }


}


//删除分组
function DelQuestionGroup(val) {
    $.ajax({
        type: "post",
        url: "/QuestionGroup/QuestionGroupDelete",
        dataType: "json",
        data: "GID=" + val,
        success: function (data) {
            //填充Datatable
            ajaxUrl = getRootPath() + "/QuestionGroup/GetQuestionGroupTable";
            //Datatable绑定数据
            DataBindTable(ajaxUrl);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAlert("提示", "分组删除失败！", "#pop_modaldialog");
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